namespace QuackersAPI_DDD.Infrastructure.Repository
{
    using Microsoft.EntityFrameworkCore;
    using QuackersAPI_DDD.Domain.Model;
    using QuackersAPI_DDD.Infrastructure.Database;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

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
    }
}