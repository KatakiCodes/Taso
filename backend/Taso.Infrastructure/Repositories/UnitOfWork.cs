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
        var entitiesWithEvents = _context.ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        // Save changes first so IDs are generated if needed
        var result = await _context.SaveChangesAsync(cancellationToken);

        // Dispatch events
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _dispatcher.DispatchAsync(domainEvent, cancellationToken);
            }
        }

        return result;
    }
}
