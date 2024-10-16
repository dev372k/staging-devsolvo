using DAL.Configurations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) : base(opt) { }

    public DbSet<Developer> Developers { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<DeveloperSkill> DeveloperSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
        modelBuilder.ApplyConfiguration(new SkillConfiguration());
        modelBuilder.ApplyConfiguration(new DeveloperSkillConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
