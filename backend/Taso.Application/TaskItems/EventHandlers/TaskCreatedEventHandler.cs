using System.Diagnostics;
using Taso.Application.Common.CQRS;
using Taso.Domain.Events;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Application.Common.Interfaces;
namespace Taso.Application.TaskItems.EventHandlers;

public class TaskCreatedEventHandler : IDomainEventHandler<TaskCreatedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public TaskCreatedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(TaskCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "TaskItem",
            entityId: domainEvent.TaskId,
            action: "Created",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
