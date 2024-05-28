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
        private readonly IPersonXChannelRepository _personXChannelRepository;
        private readonly IChannelPersonRoleXPersonXChannelRepository _channelPersonRoleXPersonXChannelRepository;
        private readonly IMessageRepository _messageRepository;

        public ChannelRepository(AppDbContext context,
            IPersonXChannelRepository personXChannelRepository,
            IChannelPersonRoleXPersonXChannelRepository channelPersonRoleXPersonXChannelRepository,
            IMessageRepository messageRepository)
        {
            _context = context;
            _personXChannelRepository = personXChannelRepository;
            _channelPersonRoleXPersonXChannelRepository = channelPersonRoleXPersonXChannelRepository;
            _messageRepository = messageRepository;
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
            var personChannels = await _context.Personxchannels
                .Where(pc => pc.Channel_Id == channel.Channel_Id)
                .ToListAsync();

            if (personChannels.Any())
            {
                _context.Personxchannels.RemoveRange(personChannels);
            }

            var channelRoles = await _context.Channelpersonrolexpersonxchannels
                .Where(cp => cp.Channel_Id == channel.Channel_Id)
                .ToListAsync();

            if (channelRoles.Any())
            {
                _context.Channelpersonrolexpersonxchannels.RemoveRange(channelRoles);
            }

            var messages = await _context.Messages
                .Where(m => m.Channel_Id == channel.Channel_Id)
                .ToListAsync();

            if (messages.Any())
            {
                _context.Messages.RemoveRange(messages);
            }

            _context.Channels.Remove(channel);

            await _context.SaveChangesAsync();
        }




        public async Task<bool> ChannelNameExists(string name)
        {
            return await _context.Channels.AnyAsync(r => r.Channel_Name == name);
        }
    }
}