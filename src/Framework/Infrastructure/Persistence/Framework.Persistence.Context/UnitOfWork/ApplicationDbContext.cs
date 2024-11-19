using System.Reflection;
using Framework.Application.Abstraction;
using Framework.Domain.Abstraction;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Framework.Persistence.Context.UnitOfWork;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>(options),
    IUnitOfWork
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseLazyLoadingProxies()
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (Assembly assembly in AssembliesReference.Assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }

    public DatabaseFacade GetDatabase()
        => Database;

    public IModel DbModel()
        => Model;

    #region UnitOfWork

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    public new void Attach<TEntity>(TEntity entity)
    {
        base.Attach(entity ?? throw new ArgumentNullException(nameof(entity)));
    }

    private void UpdateAuditableEntities()
    {
        var entries = ChangeTracker.Entries<IAuditable>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Property(p => p.CreatedDateTime).CurrentValue = DateTimeOffset.UtcNow;

            if (entry.State == EntityState.Modified)
                entry.Property(p => p.ModifiedDateTime).CurrentValue = DateTimeOffset.UtcNow;
        }
    }

    #endregion
}