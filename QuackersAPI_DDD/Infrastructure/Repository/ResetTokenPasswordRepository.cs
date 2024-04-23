using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System.Linq.Expressions;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class ResetTokenPasswordRepository : IResetTokenPasswordRepository
    {
        private readonly AppDbContext _context;

        public ResetTokenPasswordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ResetTokenPassword resetToken)
        {
            _context.ResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task<ResetTokenPassword> FindAsync(Expression<Func<ResetTokenPassword, bool>> predicate)
        {
            return await _context.ResetTokens.FirstOrDefaultAsync(predicate);
        }


        public async Task UpdateAsync(ResetTokenPassword resetToken)
        {
            _context.ResetTokens.Update(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
