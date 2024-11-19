namespace Identity.Persistence.ModelConfiguration;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(rt => rt.UserId)
            .IsRequired();

        builder.Property(rt => rt.IssuedDateTime)
            .IsRequired();

        builder.Property(rt => rt.ExpirationDateTime)
            .IsRequired();

        builder.Property(rt => rt.IsExpired)
            .IsRequired();

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.HasOne(rt => rt.User) 
        .WithMany() 
        .HasForeignKey(rt => rt.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}