namespace Identity.Persistence.ModelConfiguration
{
    public class ApplicationRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            builder.ToTable("IdentityRoleClaims"); 
        }
    }
}