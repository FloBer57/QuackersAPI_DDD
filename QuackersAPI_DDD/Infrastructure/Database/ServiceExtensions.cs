using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Service;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Infrastructure.Database
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IChannelService, ChannelService>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IChannelTypeService, ChannelTypeService>();
            services.AddScoped<IChannelTypeRepository, ChannelTypeRepository>();
            services.AddScoped<IPersonJobTitleService, PersonJobTitleService>();
            services.AddScoped<IPersonJobTitleRepository, PersonJobTitleRepository>();
            services.AddScoped<IPersonRoleService, PersonRoleService>();
            services.AddScoped<IPersonRoleRepository, PersonRoleRepository>();
            services.AddScoped<IPersonStatutService, PersonStatutService>();
            services.AddScoped<IPersonStatutRepository, PersonStatutRepository>();

            return services;
        }
    }
}
