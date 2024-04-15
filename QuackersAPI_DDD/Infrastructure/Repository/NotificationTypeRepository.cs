using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        private readonly AppDbContext _context;

        public NotificationTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationType>> GetAllNotificationTypes()
        {
            return await _context.NotificationTypes.ToListAsync();
        }

        public async Task<NotificationType> GetNotificationTypeById(int id)
        {
            return await _context.NotificationTypes.FindAsync(id);
        }

        public async Task<NotificationType> CreateNotificationType(NotificationType notificationType)
        {
            _context.NotificationTypes.Add(notificationType);
            await _context.SaveChangesAsync();
            return notificationType;
        }

        public async Task<NotificationType> UpdateNotificationType(NotificationType notificationType)
        {
            _context.Entry(notificationType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return notificationType;
        }

        public async Task<bool> DeleteNotificationType(int id)
        {
            var notificationType = await _context.NotificationTypes.FindAsync(id);
            if (notificationType != null)
            {
                _context.NotificationTypes.Remove(notificationType);
                await _context.SaveChangesAsync();
                return true; 
            }
            return false; 
        }
    }
}
