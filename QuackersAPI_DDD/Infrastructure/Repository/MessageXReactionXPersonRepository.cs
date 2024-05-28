using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class MessageXReactionXPersonRepository : IMessageXReactionXPersonRepository
    {
        private readonly AppDbContext _context;


        public MessageXReactionXPersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageXReactionXPerson>> GetAllReactions()
        {
            return await _context.Messagexreactionxpeople.ToListAsync();
        }

        public async Task<MessageXReactionXPerson> GetReactionById(int personId, int messageId, int reactionId)
        {
            return await _context.Messagexreactionxpeople
                .FirstOrDefaultAsync(x => x.Person_Id == personId && x.Message_Id == messageId && x.Reaction_Id == reactionId);
        }

        public async Task<MessageXReactionXPerson> CreateReaction(MessageXReactionXPerson reaction)
        {
            _context.Messagexreactionxpeople.Add(reaction);
            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<MessageXReactionXPerson> UpdateReaction(MessageXReactionXPerson reaction)
        {
            _context.Entry(reaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<bool> DeleteReaction(int personId, int messageId, int reactionId)
        {
            var reaction = await GetReactionById(personId, messageId, reactionId);
            if (reaction != null)
            {
                _context.Messagexreactionxpeople.Remove(reaction);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MessageXReactionXPerson>> GetReactionsByMessageId(int messageId)
        {
            return await _context.Messagexreactionxpeople
                                 .Include(mrp => mrp.Reaction) 
                                 .Where(mrp => mrp.Message_Id == messageId)
                                 .ToListAsync();
        }

    }
}
