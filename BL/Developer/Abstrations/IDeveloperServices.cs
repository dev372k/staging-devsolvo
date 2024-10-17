using BL.Developer.DTOs.Request;
using BL.Developer.DTOs.Response;

namespace BL.Developer.Abstrations;

public interface IDeveloperServices
{
    Task<GetDeveloperDto> AddAsync(AddDeveloperDto dto);

    Task<GetDeveloperDto> UpdateAsync(Guid id, UpdateDeveloperDto dto);

    Task<GetDeveloperDto> DeleteAsync(Guid id);

    Task<GetDeveloperDto> GetAsync(Guid id);

    Task<List<GetDeveloperDto>> GetAsync(int pageSize, int pageNo);
}
