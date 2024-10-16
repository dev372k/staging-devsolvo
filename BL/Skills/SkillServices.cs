using AutoMapper;
using BL.Skills.DTOs.Request;
using BL.Skills.DTOs.Response;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commons;
using SharedKernel.Exceptions;
using System.Net;

namespace BL.Skills;

public class SkillServices(ApplicationDBContext _context, IMapper _mapper)
{
    public async Task<GetSkillDto> AddAsync(AddSkillDto dto)
    {
        var skill = new Skill
        {
            Id = Guid.NewGuid(),
            Name = dto.name,
            CreatedOn = DateTime.UtcNow
        };

        _context.Set<Skill>().Add(skill);
        await _context.SaveChangesAsync();

        return new GetSkillDto(skill.Id, skill.Name);
    }

    public async Task<GetSkillDto> UpdateAsync(Guid id, UpdateSkillDto dto)
    {
        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        skill!.Name = dto.name;
        skill.UpdatedOn = DateTime.UtcNow;

        _context.Set<Skill>().Update(skill);
        await _context.SaveChangesAsync();

        return new GetSkillDto(skill.Id, skill.Name);
    }

    public async Task<GetSkillDto> DeleteAsync(Guid id)
    {
        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        var developerSkill = await _context.Set<DeveloperSkill>().FirstOrDefaultAsync(_ => _.SkillId == skill.Id);
        if (developerSkill != null)
            throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.CANNOT_DELETE);

        _context.Set<Skill>().Remove(skill);
        await _context.SaveChangesAsync();

        return new GetSkillDto(skill!.Id, skill.Name);
    }

    public async Task<GetSkillDto> GetAsync(Guid id)
    {
        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == id) ??
            throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);
        return _mapper.Map<GetSkillDto>(skill);
    }

    public async Task<GetSkillDto> GetbyDeveloperAsync(Guid developerId)
    {
        var developerSkill = await _context.Set<DeveloperSkill>().FirstOrDefaultAsync(_ => _.DeveloperId == developerId);
        if (developerSkill == null)
            throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == developerSkill!.SkillId);
        return _mapper.Map<GetSkillDto>(skill);
    }

    public async Task<GetSkillDto> UpdateAssigneeAsync(Guid id, Guid developerId)
    {
        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        var developerSkill = await _context.Set<DeveloperSkill>().FirstOrDefaultAsync(_ => _.SkillId == skill.Id && _.DeveloperId == developerId);
        if (developerSkill != null)
            _context.Set<DeveloperSkill>().Update(new DeveloperSkill
            {
                SkillId = skill.Id,
                DeveloperId = developerId,
            });
        else
            _context.Set<DeveloperSkill>().Add(new DeveloperSkill
            {
                SkillId = skill.Id,
                DeveloperId = developerId,
            });

        await _context.SaveChangesAsync();

        return new GetSkillDto(skill.Id, skill.Name);
    }

    public async Task<GetSkillDto> DeleteAssigneeAsync(Guid id, Guid developerId)
    {
        var skill = await _context.Set<Skill>().SingleOrDefaultAsync(_ => _.Id == id) ??
                    throw new CustomException(HttpStatusCode.NotFound, ExceptionMessage.RECORD_DOESNOT_EXIST);

        var developerSkill = await _context.Set<DeveloperSkill>().FirstOrDefaultAsync(_ => _.SkillId == skill.Id && _.DeveloperId == developerId);
        if (developerSkill != null)
        {
            _context.Set<DeveloperSkill>().Remove(developerSkill);
            await _context.SaveChangesAsync();
        }

        return new GetSkillDto(skill!.Id, skill.Name);
    }
}
