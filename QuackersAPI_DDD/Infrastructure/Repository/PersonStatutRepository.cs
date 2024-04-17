using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class PersonStatutRepository : IPersonStatutRepository
    {
        private readonly AppDbContext _context;

        public PersonStatutRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PersonStatut>> GetAllPersonStatuts()
        {
            return await _context.Set<PersonStatut>().ToListAsync();
        }

        public async Task<PersonStatut> GetPersonStatutById(int id)
        {
            return await _context.Set<PersonStatut>().FindAsync(id);
        }

        public async Task<PersonStatut> CreatePersonStatut(PersonStatut personStatut)
        {
            _context.Set<PersonStatut>().Add(personStatut);
            await _context.SaveChangesAsync();
            return personStatut;
        }

        public async Task<PersonStatut> UpdatePersonStatut(int id, PersonStatut personStatut)
        {
            var existingPersonStatut = await _context.Set<PersonStatut>().FindAsync(id);
            if (existingPersonStatut == null)
            {
                return null;
            }

            _context.Entry(existingPersonStatut).CurrentValues.SetValues(personStatut);
            await _context.SaveChangesAsync();

            return existingPersonStatut;
        }

        public async Task<bool> DeletePersonStatut(int id)
        {
            var existingPersonStatut = await _context.Set<PersonStatut>().FindAsync(id);
            if (existingPersonStatut == null)
            {
                return false;
            }

            _context.Set<PersonStatut>().Remove(existingPersonStatut);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PersonStatutNameExists(string name)
        {
            return await _context.Personroles.AnyAsync(r => r.PersonRole_Name == name);
        }
    }
}
