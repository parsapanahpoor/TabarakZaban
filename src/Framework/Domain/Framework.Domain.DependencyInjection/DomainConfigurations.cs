using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Framework.Domain.DependencyInjection;

public static class FrameworkDomainConfigurations
{
    public static readonly Assembly[] Assemblies =
    [

    ];

    public static IServiceCollection RegisterDomainLayer(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses(Assemblies)
            .Where(c => c.Name.EndsWith("Service"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

        foreach (Assembly assembly in Assemblies)
        {
            services.AddValidatorsFromAssembly(assembly);
        }

        //services.AddAutoMapper(Assemblies);
        return services;
    }

    public static IServiceProvider UseDomainLayer(this IServiceProvider app)
        => app;
}