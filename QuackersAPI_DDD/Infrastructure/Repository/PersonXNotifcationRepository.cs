using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class PersonXNotificationRepository : IPersonXNotificationRepository
    {
        private readonly AppDbContext _context;

        public PersonXNotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonXNotification>> GetAllAssociations()
        {
            return await _context.Personxnotifications.ToListAsync();
        }

        public async Task<PersonXNotification> GetAssociationById(int personId, int notificationId)
        {
            return await _context.Personxnotifications
                                 .FirstOrDefaultAsync(x => x.Person_Id == personId && x.Notification_Id == notificationId);
        }

        public async Task<PersonXNotification> CreateAssociation(PersonXNotification association)
        {
            _context.Personxnotifications.Add(association);
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<PersonXNotification> UpdateAssociation(PersonXNotification association)
        {
            _context.Entry(association).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<bool> DeleteAssociation(int personId, int notificationId)
        {
            var association = await GetAssociationById(personId, notificationId);
            if (association != null)
            {
                _context.Personxnotifications.Remove(association);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
