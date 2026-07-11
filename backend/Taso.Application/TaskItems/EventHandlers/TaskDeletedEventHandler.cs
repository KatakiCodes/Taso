using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Entities;
using Taso.Domain.Events;
using Taso.Domain.Repositories;

namespace Taso.Application.TaskItems.EventHandlers;

public class TaskDeletedEventHandler : IDomainEventHandler<TaskDeletedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public TaskDeletedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(TaskDeletedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "TaskItem",
            entityId: domainEvent.TaskId,
            action: "Deleted",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
