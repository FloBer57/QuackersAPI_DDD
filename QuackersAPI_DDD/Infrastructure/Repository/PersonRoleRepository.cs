using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class PersonRoleRepository : IPersonRoleRepository
    {
        private readonly AppDbContext _context;

        public PersonRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PersonRole> CreatePersonRole(PersonRole personRole)
        {
            _context.Set<PersonRole>().Add(personRole);
            await _context.SaveChangesAsync();
            return personRole;
        }

        public async Task<IEnumerable<PersonRole>> GetAllPersonRoles()
        {
            return await _context.Set<PersonRole>().ToListAsync();
        }

        public async Task<PersonRole> GetPersonRoleById(int id)
        {
            return await _context.Set<PersonRole>().FindAsync(id);
        }

        public async Task<PersonRole> UpdatePersonRole(PersonRole personRole)
        {
            _context.Entry(personRole).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return personRole;
        }

        public async Task DeletePersonRole(int id)
        {
            var personRole = await _context.Set<PersonRole>().FindAsync(id);
            if (personRole != null)
            {
                _context.Set<PersonRole>().Remove(personRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
