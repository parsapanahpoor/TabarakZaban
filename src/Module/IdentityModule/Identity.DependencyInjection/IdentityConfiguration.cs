using Identity.Core;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedProject;
using Identity.Infrastructure.Services;
using Framework.Infrastructure.Shared.MessageOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Framework.Infrastructure.Shared.TokenOption;
using Identity.Domain.Repository;
using Framework.Persistence.Context.UnitOfWork;
using Identity.Persistence.Repository;
using Identity.Application;
using Identity.Application.Services;

namespace Identity.DependencyInjection;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        AuthorizationConfiguration.EnableAuthorization = false;
        
        services.AddAuthorization(option =>
            option.AddPolicy(Constants.AdminPolicy, p => p.RequireRole(Constants.AdminRole)));
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;

                PasswordOptions(options);
                LockoutOptions(options);
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, EmailService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddIdentityApiEndpoints<ApplicationUser>();

        return services;
    }

    private static void LockoutOptions(IdentityOptions options)
    {
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
    }

    private static void PasswordOptions(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }

    public static IServiceCollection IdentityInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
        services.Configure<ApplicationTokenOption>(configuration.GetSection("Tokens"));

        var issuer = configuration["Tokens:Issuer"] ?? "";
        var audience = configuration["Tokens:Audience"] ?? "";
        var key = configuration["Tokens:Key"] ?? "";

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });

        return services;
    }
}