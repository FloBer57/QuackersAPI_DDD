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

        public async Task CreateChannel(Channel channel)
        {
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();
        }

        public async Task<Channel> GetChannelById(int id)
        {
            return await _context.Channels.FindAsync(id);
        }

        public async Task<IEnumerable<Channel>> GetAllChannel()
        {
            return await _context.Channels.ToListAsync();
        }

        public async Task UpdateChannel(Channel channel)
        {
            _context.Channels.Attach(channel);
            _context.Entry(channel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel != null)
            {
                _context.Channels.Remove(channel);
                await _context.SaveChangesAsync();
            }
        }
    }
}