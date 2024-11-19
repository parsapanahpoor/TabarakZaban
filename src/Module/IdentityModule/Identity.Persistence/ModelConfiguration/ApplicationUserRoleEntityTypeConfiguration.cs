namespace Identity.Persistence.ModelConfiguration
{
    public class ApplicationUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("IdentityUserRoles"); 
        }
    }
}