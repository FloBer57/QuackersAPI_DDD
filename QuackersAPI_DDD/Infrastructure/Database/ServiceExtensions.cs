using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Service;
using QuackersAPI_DDD.Application.Service.QuackersAPI_DDD.Application.Service;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;
using QuackersAPI_DDD.Infrastructure.Repository.QuackersAPI_DDD.Infrastructure.Repository;

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
            services.AddScoped<IChannelPersonRoleService, ChannelPersonRoleService>();
            services.AddScoped<IChannelPersonRoleRepository, ChannelPersonRoleRepository>();
            services.AddScoped<IChannelPersonRoleXPersonXChannelService, ChannelPersonRoleXPersonXChannelService>();
            services.AddScoped<IChannelPersonRoleXPersonXChannelRepository, ChannelPersonRoleXPersonXChannelRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageXReactionXPersonService, MessageXReactionXPersonService>();
            services.AddScoped<IMessageXReactionXPersonRepository, MessageXReactionXPersonRepository>();
            services.AddScoped<IPersonXChannelService, PersonXChannelService>();
            services.AddScoped<IPersonXChannelRepository, PersonXChannelRepository>();
            services.AddScoped<IPersonXNotificationService, PersonXNotificationService>();
            services.AddScoped<IPersonXNotificationRepository, PersonXNotificationRepository>();
            services.AddScoped<IPersonXMessageService, PersonXMessageService>();
            services.AddScoped<IPersonXMessageRepository, PersonXMessageRepository>();
            services.AddScoped<IReactionService, ReactionService>();
            services.AddScoped<IReactionRepository, ReactionRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();
            services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            return services;
        }
    }
}
