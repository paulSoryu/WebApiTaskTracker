using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTaskTracker.Data.Entities;
using WebApiTaskTracker.Utilities;

namespace WebApiTaskTracker.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserName)
               .IsRequired()
               .HasMaxLength(UserConstraints.NameMaxLength);

        builder.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(UserConstraints.EmailAddressMaxLength);

        builder.HasIndex(p => p.Email)
               .IsUnique();
    }
}