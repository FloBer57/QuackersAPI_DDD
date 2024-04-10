using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class ChannelTypeRepository : IChannelTypeRepository
    {
        private readonly AppDbContext _context;

        public ChannelTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChannelType>> GetAllChannelTypes()
        {
            return await _context.ChannelTypes.ToListAsync();
        }

        public async Task<ChannelType> GetChannelTypeById(int id)
        {
            return await _context.ChannelTypes.FindAsync(id);
        }

        public async Task<ChannelType> CreateChannelType(ChannelType channelType)
        {
            _context.ChannelTypes.Add(channelType);
            await _context.SaveChangesAsync();
            return channelType;
        }

        public async Task<ChannelType> UpdateChannelType(ChannelType channelType)
        {
            _context.Entry(channelType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return channelType;
        }

        public async Task DeleteChannelType(int id)
        {
            var channelType = await _context.ChannelTypes.FindAsync(id);
            if (channelType != null)
            {
                _context.ChannelTypes.Remove(channelType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
