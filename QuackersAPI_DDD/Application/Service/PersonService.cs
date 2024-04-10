using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            // Vous pouvez ajouter de la logique métier ici avant de créer la personne
            return await _personRepository.CreatePerson(person);
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _personRepository.GetAllPersons();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _personRepository.GetPersonById(id);
        }

        public async Task<Person> UpdatePerson(int id, Person person)
        {
            var existingPerson = await _personRepository.GetPersonById(id);
            if (existingPerson == null)
            {
                throw new InvalidOperationException($"Person with id {id} not found.");
            }

            // Ici, vous pouvez mettre à jour les propriétés de existingPerson selon les valeurs de person
            // puis enregistrer les changements avec votre repository

            return await _personRepository.UpdatePerson(existingPerson);
        }

        public async Task<bool> DeletePerson(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
            {
                return false;
            }

            await _personRepository.DeletePerson(id);
            return true;
        }
    }
}
