using QuackersAPI_DDD.Domain.Model;
using System.Linq.Expressions;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IResetTokenPasswordRepository
    {
        Task AddAsync(ResetTokenPassword resetToken);
        Task<ResetTokenPassword> FindAsync(Expression<Func<ResetTokenPassword, bool>> predicate);
        Task UpdateAsync(ResetTokenPassword resetToken);
        Task SaveChangesAsync();
    }
}
