using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonJobTitleService _personJobTitleService;
        private readonly IPersonStatutService _personStatutService;
        private readonly IPersonRoleService _personRoleService;

        public PersonService(IPersonRepository personRepository, IPersonJobTitleService personJobTitleService, IPersonStatutService personStatutService, IPersonRoleService personRoleService )
        {
            _personRepository = personRepository;
            _personJobTitleService = personJobTitleService;
            _personStatutService = personStatutService;
            _personRoleService = personRoleService;
        }

        public async Task<Person> CreatePerson(CreatePersonDTO createPersonDTO)
        {
            var defaultPersonRoleId = 1;
            var defaultPersonStatutId = 1;
            var defaultPersonJobTitle = 1;
            var defaultPersonDescription = $"Je suis {createPersonDTO.FirstName} {createPersonDTO.LastName} nouveau de Quacker!";
            var defaultPersonTokenResetPassword = SecurityService.GenerateToken();
            var defaultPersonCreatedTime = DateTime.Now;
            var defaultProfilePicturePath = "Path/To/Default/Image";

            var personJobTitleType = await _personJobTitleService.GetPersonJobTitleById(defaultPersonJobTitle);
            if (personJobTitleType == null)
            {
                throw new InvalidOperationException("PersonJobTitle with this ID can't be found");
            }

            var personStatutType = await _personStatutService.GetPersonStatutById(defaultPersonStatutId);
            if (personStatutType == null)
            {
                throw new InvalidOperationException("PersonStatutType with this ID can't be found");
            }

            var personRoleType = await _personRoleService.GetPersonRoleById(defaultPersonRoleId);
            if (personRoleType == null)
            {
                throw new InvalidOperationException("PersonRoleType with this ID can't be found");
            }

            var person = new Person
            {
                Person_Email = createPersonDTO.Email,
                Person_FirstName = createPersonDTO.FirstName,
                Person_LastName = createPersonDTO.LastName,
                Person_PhoneNumber = createPersonDTO.PhoneNumber,
                Person_Description = defaultPersonDescription,
                Person_TokenResetPassword = defaultPersonTokenResetPassword,
                Person_CreatedTimePerson = defaultPersonCreatedTime,
                Person_ProfilPicturePath = defaultProfilePicturePath,
                Person_Password = PasswordGenerator.GeneratePassword(12),
                PersonJobTitle_Id = defaultPersonJobTitle,
                PersonStatut_Id = defaultPersonStatutId,
                PersonRole_Id = defaultPersonRoleId,

                PersonJobTitle = personJobTitleType,
                PersonStatut = personStatutType,
                PersonRole = personRoleType,
                
            };

            personJobTitleType.People.Add(person);
            personRoleType.People.Add(person);
            personStatutType.People.Add(person);

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

            if (!string.IsNullOrWhiteSpace(updatePersonDTO.PhoneNumber))
            {
                person.Person_PhoneNumber = updatePersonDTO.PhoneNumber;
            }

            if (updatePersonDTO.Description != null)
            {
                person.Person_Description = updatePersonDTO.Description;
            }

            if (!string.IsNullOrWhiteSpace(updatePersonDTO.ProfilPicturePath) && updatePersonDTO.ProfilPicturePath != "Path/To/Default/Image")
            {
                person.Person_ProfilPicturePath = updatePersonDTO.ProfilPicturePath;
            }

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

        public async Task<IEnumerable<Person>> GetPersonsByJobTitle(int jobTitleId)
        {
            return await _personRepository.GetPersonByJobTitle(jobTitleId);
        }

        public async Task<IEnumerable<Person>> GetPersonsByStatut(int statutId)
        {
            return await _personRepository.GetPersonByStatut(statutId);
        }

        public async Task<IEnumerable<Person>> GetPersonsByRole(int roleId)
        {
            return await _personRepository.GetPersonByRole(roleId);
        }
    }
}
