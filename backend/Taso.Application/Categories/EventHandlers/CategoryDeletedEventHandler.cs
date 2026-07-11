using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Entities;
using Taso.Domain.Events;
using Taso.Domain.Repositories;

namespace Taso.Application.Categories.EventHandlers;

public class CategoryDeletedEventHandler : IDomainEventHandler<CategoryDeletedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public CategoryDeletedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(CategoryDeletedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "Category",
            entityId: domainEvent.CategoryId,
            action: "Deleted",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
