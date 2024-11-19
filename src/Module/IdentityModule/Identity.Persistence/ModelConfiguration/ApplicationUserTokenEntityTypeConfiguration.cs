namespace Identity.Persistence.ModelConfiguration
{
    public class ApplicationUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("IdentityUserTokens"); 
        }
    }
}