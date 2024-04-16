using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class ChannelPersonRoleRepository : IChannelPersonRoleRepository
    {
        private readonly AppDbContext _context;

        public ChannelPersonRoleRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ChannelPersonRole>> GetAllChannelPersonRoles()
        {
            return await _context.ChannelPersonRoles.ToListAsync();
        }

        public async Task<ChannelPersonRole> GetChannelPersonRoleById(int id)
        {
            return await _context.ChannelPersonRoles.FindAsync(id);
        }

        public async Task<ChannelPersonRole> CreateChannelPersonRole(ChannelPersonRole channelPersonRole)
        {
            _context.ChannelPersonRoles.Add(channelPersonRole);
            await _context.SaveChangesAsync();
            return channelPersonRole;
        }

        public async Task<ChannelPersonRole> UpdateChannelPersonRole(ChannelPersonRole channelPersonRole)
        {
            _context.Entry(channelPersonRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return channelPersonRole;
        }

        public async Task DeleteChannelPersonRole(int id)
        {
            var channelPersonRole = await _context.ChannelPersonRoles.FindAsync(id);
            if (channelPersonRole != null)
            {
                _context.ChannelPersonRoles.Remove(channelPersonRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ChannelPersonRoleNameExists(string roleName)
        {
            return await _context.ChannelPersonRoles.AnyAsync(r => r.ChannelPersonRole_Name == roleName);
        }
    }
}
