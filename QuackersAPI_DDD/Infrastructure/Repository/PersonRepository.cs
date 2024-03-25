namespace QuackersAPI_DDD.Infrastructure.Repository
{
    using Microsoft.EntityFrameworkCore;
    using QuackersAPI_DDD.Domain.Model;
    using QuackersAPI_DDD.Infrastructure.Database;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllPerson()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            _context.Person.Attach(person);
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}