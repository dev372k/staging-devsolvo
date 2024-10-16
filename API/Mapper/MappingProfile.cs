using AutoMapper;
using BL.Skills.DTOs.Response;
using DAL.Entities;
using BL.Developer.DTOs.Response;

namespace API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Skill, GetSkillDto>();
        CreateMap<Developer, GetDeveloperDto>();
    }
}
