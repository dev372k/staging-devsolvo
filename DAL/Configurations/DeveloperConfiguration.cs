using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        // Primary Key
        builder.HasKey(d => d.Id);

        // Property Configuration
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Surname)
            .IsRequired()
            .HasMaxLength(100);

        // Relationship with DeveloperSkill
        builder.HasMany(d => d.DeveloperSkills)
               .WithOne(ds => ds.Developer)
               .HasForeignKey(ds => ds.DeveloperId);
    }
}