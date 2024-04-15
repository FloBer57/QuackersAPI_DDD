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

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _context.Messages
                .Include(m => m.Attachments)
                .Include(m => m.Messagexreactionxpeople)
                .Include(m => m.Person)
                .Include(m => m.Channel)
                .ToListAsync();
        }

        public async Task<Message> GetMessageById(int messageId)
        {
            return await _context.Messages
                .Include(m => m.Attachments)
                .Include(m => m.Messagexreactionxpeople)
                .Include(m => m.Person)
                .Include(m => m.Channel)
                .FirstOrDefaultAsync(m => m.Message_Id == messageId);
        }

        public async Task<Message> CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> UpdateMessage(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task DeleteMessage(int messageId)
        {
            var message = await GetMessageById(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
