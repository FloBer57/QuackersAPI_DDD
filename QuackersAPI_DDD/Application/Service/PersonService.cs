using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
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

        public async Task<Person> CreatePerson(CreatePersonDTO createPersonDTO)
        {
            var person = new Person
            {
                Person_Email = createPersonDTO.Email,
                Person_FirstName = createPersonDTO.FirstName,
                Person_LastName = createPersonDTO.LastName,
                Person_PhoneNumber = createPersonDTO.PhoneNumber,
                Person_Description = createPersonDTO.Description,
                Person_Password = PasswordGenerator.GeneratePassword(12),
            };

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

        public async Task<Person> UpdatePerson(int id, UpdatePersonDTO updatePersonDTO)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
            {
                throw new InvalidOperationException($"Person with id {id} not found.");
            }

            person.Person_PhoneNumber = updatePersonDTO.PhoneNumber ?? person.Person_PhoneNumber;
            person.Person_Description = updatePersonDTO.Description ?? person.Person_Description;
            person.Person_ProfilPicturePath = updatePersonDTO.ProfilPicturePath ?? person.Person_ProfilPicturePath;

            return await _personRepository.UpdatePerson(person);
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
