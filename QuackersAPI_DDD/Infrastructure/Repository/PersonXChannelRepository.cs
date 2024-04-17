namespace QuackersAPI_DDD.Infrastructure.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::QuackersAPI_DDD.Domain.Model;
    using global::QuackersAPI_DDD.Infrastructure.Database;
    using global::QuackersAPI_DDD.Infrastructure.InterfaceRepository;

    namespace QuackersAPI_DDD.Infrastructure.Repository
    {
        public class PersonXChannelRepository : IPersonXChannelRepository
        {
            private readonly AppDbContext _context;

            public PersonXChannelRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<PersonXChannel>> GetAllAssociations()
            {
                return await _context.Personxchannels.ToListAsync();
            }

            public async Task<PersonXChannel> GetAssociationById(int personId, int channelId)
            {
                return await _context.Personxchannels
                .FirstOrDefaultAsync(p => p.Person_Id == personId && p.Channel_Id == channelId);
            }

            public async Task<PersonXChannel> CreateAssociation(PersonXChannel association)
            {
                _context.Personxchannels.Add(association);
                await _context.SaveChangesAsync();
                return association;
            }

            public async Task<PersonXChannel> UpdateAssociation(PersonXChannel association)
            {
                _context.Entry(association).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return association;
            }

            public async Task<bool> DeleteAssociation(int personId, int channelId)
            {
                var association = await GetAssociationById(personId, channelId);
                if (association != null)
                {
                    _context.Personxchannels.Remove(association);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
    }

}
