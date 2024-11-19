using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Application.DependencyInjection;

public static class FrameworkApplicationConfigurations
{
    public static readonly Assembly[] Assemblies =
    [
        //IdentityApplicationAssemblyReference.Assembly,
    ];

    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        foreach (var assembly in Assemblies)
        {
            services.AddValidatorsFromAssembly(assembly);
            //services.AddMediatR(config =>
            //{
            //    config.RegisterServicesFromAssembly(assembly);
            //    config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            //});
        }

        //services.AddAutoMapper(Assemblies);
        return services;
    }

    public static IServiceProvider UseApplicationLayer(this IServiceProvider app)
    {
        return app;
    }
}