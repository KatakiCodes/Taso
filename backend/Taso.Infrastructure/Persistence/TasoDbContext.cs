using Microsoft.EntityFrameworkCore;
using Taso.Domain.Entities;

namespace Taso.Infrastructure.Persistence;

public class TasoDbContext : DbContext
{
    public TasoDbContext(DbContextOptions<TasoDbContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
