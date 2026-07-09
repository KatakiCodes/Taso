using System.Diagnostics;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;

namespace Taso.Infrastructure.Services;

public class SimpleDomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Simplest implementation until custom CQRS dispatcher is introduced
        Debug.WriteLine($"[DomainEvent Dispatched] {domainEvent.GetType().Name}");
        return Task.CompletedTask;
    }
}
