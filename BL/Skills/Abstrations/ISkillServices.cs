using BL.Skills.DTOs.Request;
using BL.Skills.DTOs.Response;

namespace BL.Skills.Abstrations;

public interface ISkillServices
{
    Task<GetSkillDto> AddAsync(AddSkillDto dto);
    Task<GetSkillDto> UpdateAsync(Guid id, UpdateSkillDto dto);

    Task<GetSkillDto> DeleteAsync(Guid id);

    Task<GetSkillDto> GetAsync(Guid id);

    Task<GetSkillDto> GetbyDeveloperAsync(Guid developerId);

    Task<GetSkillDto> UpdateAssigneeAsync(Guid id, Guid developerId);

    Task<GetSkillDto> DeleteAssigneeAsync(Guid id, Guid developerId);
}
