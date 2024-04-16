namespace QuackersAPI_DDD.Infrastructure.Repository
{
    using global::QuackersAPI_DDD.Domain.Model;
    using global::QuackersAPI_DDD.Infrastructure.Database;
    using global::QuackersAPI_DDD.Infrastructure.InterfaceRepository;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ChannelRepository : IChannelRepository
    {
        private readonly AppDbContext _context;

        public ChannelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Channel> CreateChannel(Channel channel)
        {
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();
            return channel;
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            return await _context.Channels
                .Include(c => c.ChannelType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Channel>> GetChannelsByChannelType(int channelTypeId)
        {
            return await _context.Channels
                .Where(c => c.ChannelType_Id == channelTypeId)
                .ToListAsync();
        }

        public async Task<Channel> GetChannelById(int id)
        {
            return await _context.Channels
                .Include(c => c.ChannelType)
                .FirstOrDefaultAsync(c => c.Channel_Id == id);
        }

        public async Task<Channel> UpdateChannel(Channel channel)
        {
            _context.Entry(channel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return channel;
        }

        public async Task DeleteChannel(Channel channel)
        {
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChannelNameExists(string name)
        {
            return await _context.Channels.AnyAsync(r => r.Channel_Name == name);
        }
    }
}