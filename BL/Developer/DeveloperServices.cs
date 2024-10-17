using DAL;
using SharedKernel.Commons;
using SharedKernel.Exceptions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using BL.Developer.DTOs.Request;
using BL.Developer.DTOs.Response;
using AutoMapper;
using DAL.Entities;
using BL.Developer.Abstrations;

namespace BL.Developer;

public class DeveloperServices(ApplicationDBContext _context, IMapper _mapper) : IDeveloperServices
{
    public async Task<GetDeveloperDto> AddAsync(AddDeveloperDto dto)
    {
        var developer = new DAL.Entities.Developer
        {
            Id = Guid.NewGuid(),
            Name = dto.name,
            Surname = dto.surname,
            CreatedOn = DateTime.UtcNow
        };

        _context.Set<DAL.Entities.Developer>().Add(developer);
        await _context.SaveChangesAsync();

        return new GetDeveloperDto(developer.Id, developer.Name, developer.Surname);
    }

    public async Task<GetDeveloperDto> UpdateAsync(Guid id, UpdateDeveloperDto dto)
    {
        var developer = await _context.Set<DAL.Entities.Developer>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        developer!.Name = dto.name;
        developer!.Surname = dto.surname;

        _context.Set<DAL.Entities.Developer>().Update(developer);
        await _context.SaveChangesAsync();

        return new GetDeveloperDto(developer.Id, developer.Name, developer.Surname);
    }

    public async Task<GetDeveloperDto> DeleteAsync(Guid id)
    {
        var developer = await _context.Set<DAL.Entities.Developer>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        _context.Set<DAL.Entities.Developer>().Remove(developer);
        await _context.SaveChangesAsync();

        return new GetDeveloperDto(developer!.Id, developer.Name, developer.Surname);
    }

    public async Task<GetDeveloperDto> GetAsync(Guid id)
    {
        var developer = await _context.Set<DAL.Entities.Developer>().SingleOrDefaultAsync(_ => _.Id == id) ??
            throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);
        return _mapper.Map<GetDeveloperDto>(developer);
    }

    public async Task<List<GetDeveloperDto>> GetAsync(int pageSize, int pageNo)
    {
        if (pageSize <= 0 || pageNo <= 0)
            throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessage.PAGINATION_ERROR);

        var developers = await _context.Set<DAL.Entities.Developer>()
                                       .Skip((pageNo - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

        return _mapper.Map<List<GetDeveloperDto>>(developers);
    }

}
