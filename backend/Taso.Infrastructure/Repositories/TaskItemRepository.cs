using Microsoft.EntityFrameworkCore;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Infrastructure.Persistence;

namespace Taso.Infrastructure.Repositories;

public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
{
    public TaskItemRepository(TasoDbContext context) : base(context)
    {
    }

    public async Task<TaskItem?> GetTaskWithSubTasksAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.SubTasks)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
