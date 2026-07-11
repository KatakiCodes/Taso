using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;

namespace Taso.Infrastructure.Services;

public class SimpleDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        
        // Uso de Reflection para invocar o método tipado e evitar a palavra-chave 'dynamic' (Tech Debt)
        var method = typeof(SimpleDomainEventDispatcher)
            .GetMethod(nameof(DispatchStronglyTypedAsync), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .MakeGenericMethod(eventType);

        var task = (Task)method.Invoke(this, new object[] { domainEvent, cancellationToken })!;
        await task;
    }

    private async Task DispatchStronglyTypedAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken)
        where TEvent : IDomainEvent
    {
        // Obter todos os handlers registrados com segurança de tipos
        var handlers = _serviceProvider.GetServices<Taso.Application.Common.CQRS.IDomainEventHandler<TEvent>>();

        // Executar todos os handlers concorrentemente para máxima performance
        var tasks = handlers.Select(handler => handler.HandleAsync(domainEvent, cancellationToken));
        await Task.WhenAll(tasks);
    }
}
