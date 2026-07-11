using System.Diagnostics;
using Taso.Application.Common.CQRS;
using Taso.Domain.Events;

namespace Taso.Application.TaskItems.EventHandlers;

public class TaskCreatedEventHandler : IDomainEventHandler<TaskCreatedEvent>
{
    public Task HandleAsync(TaskCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Apenas para fins de demonstração e verificação de que o pipeline está funcionando
        Debug.WriteLine($"[DOMAIN EVENT EXECUTADO] A tarefa com ID {domainEvent.TaskId} foi criada com sucesso.");
        
        return Task.CompletedTask;
    }
}
