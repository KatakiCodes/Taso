using Taso.Domain.Entities;

namespace Taso.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IReadOnlyList<Category>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
