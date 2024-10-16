namespace DAL.Entities;

public class Skill : Base<Guid>
{
    public required string Name { get; set; }
    public ICollection<DeveloperSkill> DeveloperSkills { get; set; }
}

