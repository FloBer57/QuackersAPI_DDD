using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateMessageOnChannel(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _context.Message.FindAsync(id);
        }

        public async Task UpdateMessage(Message message)
        {
            _context.Message.Attach(message);
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task DeleteAllMessageFromChannel(int id)
        {
            throw new NotImplementedException(); /* A FAIRE DANS CHANNEL */ /* ZEBI */
        }

        public Task DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAllMessageByChannelId()  /* A FAIRE DANS CHANNEL */ /* ZEBI */
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAllMessageByPerson()  /* A FAIRE DANS Person */ /* ZEBI */
        {
            throw new NotImplementedException();
        }
    }
}
