using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taso.Domain.Entities;
using Taso.Infrastructure.Identity;

namespace Taso.Infrastructure.Persistence;

public class TasoDbContext : IdentityDbContext<ApplicationUser>
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
