using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Repositories;
using Taso.Infrastructure.CQRS;
using Taso.Infrastructure.Persistence;
using Taso.Infrastructure.Repositories;
using Taso.Infrastructure.Services;
using Taso.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;

namespace Taso.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<TasoDbContext>(options =>
            options.UseInMemoryDatabase("TasoDb"));

        services.AddScoped<IDomainEventDispatcher, SimpleDomainEventDispatcher>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISender, Sender>();

        services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
            .AddEntityFrameworkStores<TasoDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<JwtTokenGenerator>();

        return services;
    }
}
