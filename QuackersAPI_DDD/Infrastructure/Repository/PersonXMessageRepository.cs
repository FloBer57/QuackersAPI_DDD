using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class PersonXMessageRepository : IPersonXMessageRepository
    {
        private readonly AppDbContext _context;

        public PersonXMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonXMessage>> GetAllAssociations()
        {
            return await _context.Personxmessages
                                 .Include(p => p.Person)
                                 .Include(m => m.Message)
                                 .ToListAsync();
        }

        public async Task<PersonXMessage> GetAssociationById(int personId, int messageId)
        {
            return await _context.Personxmessages
                                 .Include(p => p.Person)
                                 .Include(m => m.Message)
                                 .FirstOrDefaultAsync(p => p.Person_Id == personId && p.Message_Id == messageId);
        }

        public async Task<PersonXMessage> CreateAssociation(PersonXMessage association)
        {
            _context.Personxmessages.Add(association);
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<PersonXMessage> UpdateAssociation(PersonXMessage association)
        {
            _context.Entry(association).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<bool> DeleteAssociation(int personId, int messageId)
        {
            var association = await GetAssociationById(personId, messageId);
            if (association != null)
            {
                _context.Personxmessages.Remove(association);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
