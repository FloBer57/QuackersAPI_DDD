using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class ChannelPersonRoleXPersonXChannelRepository : IChannelPersonRoleXPersonXChannelRepository
    {
        private readonly AppDbContext _context;

        public ChannelPersonRoleXPersonXChannelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChannelPersonRoleXPersonXChannel>> GetAllAssociations()
        {
            return await _context.Channelpersonrolexpersonxchannels
                .Include(x => x.Person)
                .Include(x => x.Channel)
                .Include(x => x.ChannelPersonRole)
                .ToListAsync();
        }

        public async Task<ChannelPersonRoleXPersonXChannel> GetAssociationByIds(int personId, int channelId)
        {
            return await _context.Channelpersonrolexpersonxchannels
                .Include(x => x.Person)
                .Include(x => x.Channel)
                .Include(x => x.ChannelPersonRole)
                .FirstOrDefaultAsync(x => x.Person_Id == personId && x.Channel_Id == channelId);
        }

        public async Task<ChannelPersonRoleXPersonXChannel> CreateAssociation(ChannelPersonRoleXPersonXChannel association)
        {
            _context.Channelpersonrolexpersonxchannels.Add(association);
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<ChannelPersonRoleXPersonXChannel> UpdateAssociation(ChannelPersonRoleXPersonXChannel association)
        {
            _context.Entry(association).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return association;
        }

        public async Task<bool> DeleteAssociation(int personId, int channelId)
        {
            var association = await GetAssociationByIds(personId, channelId);
            if (association != null)
            {
                _context.Channelpersonrolexpersonxchannels.Remove(association);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddPersonRoleToChannel(int personId, int channelId)
        {
            var association = await GetAssociationByIds(personId,channelId);
            if (association != null)
            {
                _context.Channelpersonrolexpersonxchannels.Add(association);
                await _context.SaveChangesAsync();
            }
        }
    }
}
