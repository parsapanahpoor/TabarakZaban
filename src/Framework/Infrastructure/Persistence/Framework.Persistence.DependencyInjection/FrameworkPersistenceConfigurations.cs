using Framework.Application.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Framework.Persistence.Context.UnitOfWork;
using System.Reflection;
using Identity.Persistence;
using Identity.Infrastructure;
using Organization.Persistence;
using BaseInformation._Persistence;

namespace Framework.Persistence.DependencyInjection;

public static class FrameworkPersistenceConfigurations
{
    public static readonly Assembly[] Assemblies =
    [
        IdentityPersistenceAssemblyReference.Assembly,
        OrganizationPersistenceAssemblyReference.Assembly,
        BaseInformationPersistenceAssemblyReference.Assembly,
    ];

    public static IServiceCollection RegisterPersistenceLayer(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction)
    {
        services.RegisterAssemblyPublicNonGenericClasses(Assemblies)
            .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Repository"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

        services.AddDbContext<ApplicationDbContext>(optionsAction);
        services.AddScoped<IUnitOfWork, ApplicationDbContext>();
        services.AddAutoMapper(Assemblies);
        return services;
    }

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        await IdentitySeedConfiguration.Seed(serviceProvider).ConfigureAwait(false);
    }
}