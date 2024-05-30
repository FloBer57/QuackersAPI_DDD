using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class PersonJobTitleRepository : IPersonJobTitleRepository
    {
        private readonly AppDbContext _context;

        public PersonJobTitleRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PersonJobTitle> CreatePersonJobTitle(PersonJobTitle personJobTitle)
        {
            _context.Personjobtitles.Add(personJobTitle);
            await _context.SaveChangesAsync();
            return personJobTitle;
        }

        public async Task<IEnumerable<PersonJobTitle>> GetAllPersonJobTitle()
        {
            return await _context.Personjobtitles.ToListAsync();
        }

        public async Task<PersonJobTitle> GetPersonJobTitleById(int id)
        {
            return await _context.Personjobtitles.FindAsync(id);
        }

        public async Task<PersonJobTitle> UpdatePersonJobTitle(PersonJobTitle personJobTitle)
        {
            _context.Personjobtitles.Update(personJobTitle);
            await _context.SaveChangesAsync();
            return personJobTitle;
        }

        public async Task DeletePersonJobTitle(PersonJobTitle personJobTitle)
        {
            _context.Personjobtitles.Remove(personJobTitle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PersonJobTitleNameExists(string name)
        {
            return await _context.Personjobtitles.AnyAsync(r => r.PersonJobTitle_Name == name);
        }
    }
}
