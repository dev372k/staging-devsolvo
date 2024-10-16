﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        // Primary Key
        builder.HasKey(s => s.Id);

        // Property Configuration
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Relationship with DeveloperSkill
        builder.HasMany(s => s.DeveloperSkills)
               .WithOne(ds => ds.Skill)
               .HasForeignKey(ds => ds.SkillId);
    }
}
