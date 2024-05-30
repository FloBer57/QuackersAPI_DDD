using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly AppDbContext _context;

        public ReactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reaction>> GetAllReactions()
        {
            return await _context.Reactions.ToListAsync();
        }

        public async Task<Reaction> GetReactionById(int id)
        {
            return await _context.Reactions.FirstOrDefaultAsync(r => r.Reaction_Id == id);
        }

        public async Task<Reaction> CreateReaction(Reaction reaction)
        {
            _context.Reactions.Add(reaction);
            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<Reaction> UpdateReaction(Reaction reaction)
        {
            _context.Entry(reaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<bool> DeleteReaction(int id)
        {
            var reaction = await _context.Reactions.FindAsync(id);
            if (reaction == null)
            {
                return false;
            }

            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactionNameExist(string name)
        {
            return await _context.Reactions.AnyAsync(r => r.Reaction_Name == name);
        }
    }
}
