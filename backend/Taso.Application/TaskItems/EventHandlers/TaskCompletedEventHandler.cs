using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Entities;
using Taso.Domain.Events;
using Taso.Domain.Repositories;

namespace Taso.Application.TaskItems.EventHandlers;

public class TaskCompletedEventHandler : IDomainEventHandler<TaskCompletedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public TaskCompletedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(TaskCompletedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "TaskItem",
            entityId: domainEvent.TaskId,
            action: "Completed",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
