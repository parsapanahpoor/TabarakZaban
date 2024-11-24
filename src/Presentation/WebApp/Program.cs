using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SharedProject;
using Framework.Domain.DependencyInjection;
using Framework.Application.DependencyInjection;
using SharedProject.FluentValidationConfiguration;
using System.Globalization;
using Framework.Persistence.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Framework.Persistence.Context.UnitOfWork;
using Identity.DependencyInjection;
using Identity.Core;

namespace WebApp;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllersWithViews();

		SetupLocalization(builder.Services);
		ConfigureServices(builder);
		RegisterServices(builder);
		builder.Services.AddAntiforgery();

		var app = builder.Build();
		ConfigurePipeline(app);
		await Seed(app);
		await app.RunAsync();
	}

	public static void SetupLocalization(IServiceCollection services)
	{
		services.AddLocalization(opt =>
		{
			opt.ResourcesPath = "";
		});

		services.Configure<RequestLocalizationOptions>(options =>
		{
			var supportedCultures = new List<CultureInfo>
			{
				new CultureInfo("en-US"),
				new CultureInfo("fa-IR")
			};

			options.DefaultRequestCulture = new RequestCulture("fa-IR");
			options.SupportedCultures = supportedCultures;
			options.SupportedUICultures = supportedCultures;

			options.ApplyCurrentCultureToResponseHeaders = true;
		});

		ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
	}

	private static void ConfigureServices(WebApplicationBuilder builder)
	{
		builder.Services
			.AddEndpointsApiExplorer()
			.AddIdentityConfiguration()
			.IdentityInfrastructureConfiguration(builder.Configuration)
			.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
	}

	private static void RegisterServices(WebApplicationBuilder builder)
	{
		builder.Services
			.RegisterDomainLayer()
			.RegisterApplicationLayer()
			.RegisterPersistenceLayer(options =>
			{
				string? connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");
				options.UseSqlServer(connectionString,
					contextOptionsBuilder => { contextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); });
			});
	}

	private static void ConfigurePipeline(WebApplication app)
	{
		var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
		app.UseRequestLocalization(options!.Value);

		app.UseDefaultFiles();
		app.UseStaticFiles();

		if (app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		if (AuthorizationConfiguration.EnableAuthorization)
			app.UseAuthorization();

		app.UseStaticFiles();

		app.UseRouting();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Services.UseDomainLayer();
		app.UseAntiforgery();
	}

	private static async Task Seed(WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var serviceProvider = scope.ServiceProvider;
		await FrameworkPersistenceConfigurations.SeedAsync(serviceProvider);
	}
}
