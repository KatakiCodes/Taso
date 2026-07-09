using Taso.Domain.Entities;

namespace Taso.Domain.Repositories;

public interface ITaskItemRepository : IRepository<TaskItem>
{
    Task<TaskItem?> GetTaskWithSubTasksAsync(Guid id, CancellationToken cancellationToken = default);
}
