using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Entities;
using Taso.Domain.Events;
using Taso.Domain.Repositories;

namespace Taso.Application.TaskItems.EventHandlers;

public class TaskUpdatedEventHandler : IDomainEventHandler<TaskUpdatedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public TaskUpdatedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(TaskUpdatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "TaskItem",
            entityId: domainEvent.TaskId,
            action: "Updated",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
