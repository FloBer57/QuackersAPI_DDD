using Microsoft.EntityFrameworkCore;

using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Configuration;

namespace QuackersAPI_DDD.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Définir les DbSet pour chaque type d'entité
        public DbSet<Person> Person { get; set; }
        /* public DbSet<PersonStatut> PersonStatuts { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<PersonJobTitle> PersonJobTitles { get; set; }
        public DbSet<PersonXNotification> PersonXNotifications { get; set; }
        public DbSet<PersonXMessage> UsersXMessages { get; set; }
        public DbSet<PersonXLoggedIn> PersonXLoggedIns { get; set; }
        public DbSet<PersonXChannel> PersonXChannels { get; set; }
        public DbSet<ChannelRolePerson> ChannelPersonRoles { get; set; }
        public DbSet<ChannelType> ChannelTypes { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelRolePersonXPersonXChannel> ChannelRolePersonXPersonXChannels { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Logged> Loggeds { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<MessageXReactionXPerson> MessageXReactionXUsers { get; set; }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations pour chaque table
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonModelConfig).Assembly);

        }
    }
}
