using API.Mapper;
using AutoMapper;
using BL.Developer;
using BL.Skills;
using DAL;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Commons;

namespace API;

public static class ConfigureServices
{
    public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
    {
        services.Services(configuration);
        services.Database(configuration);
        services.Misc(configuration);
    }

    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SkillServices>();
        services.AddScoped<DeveloperServices>();
    }

    public static void Misc(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddCors(opt =>
        {
            opt.AddPolicy(name: DevContants.CORS, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddSingleton(new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        }).CreateMapper());
    }

    public static void Database(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDBContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("cs"));
        });
    }
}
