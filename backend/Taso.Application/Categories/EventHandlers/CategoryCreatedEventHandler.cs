using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Entities;
using Taso.Domain.Events;
using Taso.Domain.Repositories;

namespace Taso.Application.Categories.EventHandlers;

public class CategoryCreatedEventHandler : IDomainEventHandler<CategoryCreatedEvent>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ICurrentUserService _currentUserService;

    public CategoryCreatedEventHandler(IAuditLogRepository auditLogRepository, ICurrentUserService currentUserService)
    {
        _auditLogRepository = auditLogRepository;
        _currentUserService = currentUserService;
    }

    public Task HandleAsync(CategoryCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var auditLog = new AuditLog(
            entityType: "Category",
            entityId: domainEvent.CategoryId,
            action: "Created",
            userId: _currentUserService.UserId ?? "System"
        );

        _auditLogRepository.Add(auditLog);
        
        return Task.CompletedTask;
    }
}
