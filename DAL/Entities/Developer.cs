namespace DAL.Entities;

public class Developer : Base<Guid>
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public ICollection<DeveloperSkill> DeveloperSkills { get; set; }
}
