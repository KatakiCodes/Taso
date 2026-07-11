using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Infrastructure.Persistence;

namespace Taso.Infrastructure.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly TasoDbContext _context;

    public AuditLogRepository(TasoDbContext context)
    {
        _context = context;
    }

    public void Add(AuditLog auditLog)
    {
        _context.AuditLogs.Add(auditLog);
    }
}
