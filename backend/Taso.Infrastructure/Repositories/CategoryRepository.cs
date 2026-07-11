using Microsoft.EntityFrameworkCore;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Infrastructure.Persistence;

namespace Taso.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(TasoDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Category>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
