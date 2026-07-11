using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Repositories;
using Taso.Infrastructure.Persistence;

namespace Taso.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TasoDbContext _context;
    private readonly IDomainEventDispatcher _dispatcher;

    public UnitOfWork(TasoDbContext context, IDomainEventDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        // Se estivermos usando o InMemory Database (para testes locais rápidos), as transações não são suportadas.
        // No mundo real (SQL Server, Postgres, SQLite), abrimos a transação.
        var useTransaction = _context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory";
        var transaction = useTransaction ? await _context.Database.BeginTransactionAsync(cancellationToken) : null;

        try
        {
            // Event Loop: dispara os eventos de domínio ANTES de salvar
            // Assim, qualquer modificação feita pelos handlers (ex: logs, atualizações em cascata)
            // entrará na mesma transação de banco.
            while (true)
            {
                var domainEventEntity = _context.ChangeTracker.Entries<BaseEntity>()
                    .Where(e => e.Entity.DomainEvents.Any())
                    .Select(e => e.Entity)
                    .FirstOrDefault();

                if (domainEventEntity == null) 
                    break;

                var events = domainEventEntity.DomainEvents.ToList();
                domainEventEntity.ClearDomainEvents();

                foreach (var domainEvent in events)
                {
                    await _dispatcher.DispatchAsync(domainEvent, cancellationToken);
                }
            }

            // Agora sim, salva tudo de uma vez
            var result = await _context.SaveChangesAsync(cancellationToken);

            // Se chegou até aqui sem exceções, comita a transação
            if (transaction != null)
                await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch
        {
            // Ocorreu alguma falha no SaveChanges ou durante o processamento de algum Domain Event!
            if (transaction != null)
                await transaction.RollbackAsync(cancellationToken);
            
            throw; // Repassa a exceção para que o middleware global/controller possa tratá-la
        }
        finally
        {
            if (transaction != null)
                await transaction.DisposeAsync();
        }
    }
}
