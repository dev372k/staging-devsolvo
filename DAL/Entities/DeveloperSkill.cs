namespace DAL.Entities;

public class DeveloperSkill : Base<Guid>
{
    public Guid DeveloperId { get; set; }
    public Guid SkillId { get; set; }

    public Developer Developer { get; set; }
    public Skill Skill { get; set; }
}

