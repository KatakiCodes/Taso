using Taso.Domain.Entities;

namespace Taso.Domain.Repositories;

public interface IAuditLogRepository
{
    void Add(AuditLog auditLog);
}
