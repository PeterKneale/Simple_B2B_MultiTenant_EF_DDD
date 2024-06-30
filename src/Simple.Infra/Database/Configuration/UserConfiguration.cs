using Simple.Domain.Users;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(UsersTable);

        builder.Property(e => e.Email)
            .ValueGeneratedNever()
            .HasColumnName(EmailColumn)
            .HasConversion<EmailConverter>();

        builder.Property(e => e.UserId)
            .ValueGeneratedNever()
            .HasColumnName(UserIdColumn)
            .HasConversion(new UserIdConverter());
        
        builder.Property(e => e.CreatedAt)
            .HasColumnName(CreatedAtColumn);

        builder.Property(e => e.Password)
            .HasMaxLength(MaxPasswordLength)
            .HasColumnName(PasswordColumn)
            .HasConversion<PasswordConverter>();
        
        builder
            .Property(e => e.TenantId)
            .HasColumnName(TenantIdColumn)
            .HasConversion<TenantIdConverter>();
       
        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(property => property.First)
                .HasMaxLength(NameMaxLength)
                .HasColumnName(FirstNameColumn);

            name.Property(property => property.Last)
                .HasMaxLength(NameMaxLength)
                .HasColumnName(LastNameColumn);
        });
    }
}