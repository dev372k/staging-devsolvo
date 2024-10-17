using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations;

public class DeveloperSkillConfiguration : IEntityTypeConfiguration<DeveloperSkill>
{
    public void Configure(EntityTypeBuilder<DeveloperSkill> builder)
    {
        builder.HasKey(ds => ds.Id);

        builder.HasOne(ds => ds.Developer)
               .WithMany(d => d.DeveloperSkills)
               .HasForeignKey(ds => ds.DeveloperId);

        builder.HasOne(ds => ds.Skill)
               .WithMany(s => s.DeveloperSkills)
               .HasForeignKey(ds => ds.SkillId);
    }
}
