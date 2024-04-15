namespace QuackersAPI_DDD.Infrastructure.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::QuackersAPI_DDD.Infrastructure.InterfaceRepository;
    using global::QuackersAPI_DDD.Domain.Model;
    using global::QuackersAPI_DDD.Infrastructure.Database;

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

        public async Task<IEnumerable<Person>> GetPersonByJobTitle(int jobTitleId)
        {
            return await _context.Persons
                .Where(p => p.PersonJobTitle_Id == jobTitleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonByStatut(int statutId)
        {
            return await _context.Persons
                .Where(p => p.PersonStatut_Id == statutId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonByRole(int roleId)
        {
            return await _context.Persons
                .Where(p => p.PersonRole_Id == roleId)
                .ToListAsync();
        }
    }
}