using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTaskTracker.Data.Entities;

namespace WebApiTaskTracker.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(p => p.Id);

        builder.HasOne(t => t.User)
               .WithMany(u => u.Tasks)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Category)
               .WithMany(c => c.Tasks)
               .HasForeignKey(t => t.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Description)
               .HasMaxLength(2000);

        builder.Property(p => p.DueDate)
               .IsRequired();

        builder.Property(p => p.Priority)
               .IsRequired();
    }
}