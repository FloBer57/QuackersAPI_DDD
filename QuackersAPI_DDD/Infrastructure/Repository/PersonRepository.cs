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

        public async Task<Person> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task DeletePerson(int id)
        {
            var person = await GetPersonById(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}