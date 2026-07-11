using Taso.Domain.Common;

namespace Taso.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string EntityType { get; private set; } = string.Empty;
    public Guid EntityId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;

    private AuditLog() { } // For EF Core

    public AuditLog(string entityType, Guid entityId, string action, string userId)
    {
        EntityType = entityType;
        EntityId = entityId;
        Action = action;
        UserId = userId;
    }
}
