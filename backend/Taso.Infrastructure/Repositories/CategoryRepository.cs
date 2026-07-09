using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Infrastructure.Persistence;

namespace Taso.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(TasoDbContext context) : base(context)
    {
    }
}
