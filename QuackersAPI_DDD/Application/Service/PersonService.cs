using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonJobTitleService _personJobTitleService;
        private readonly IPersonStatutService _personStatutService;
        private readonly IPersonRoleService _personRoleService;
        private readonly ISecurityService _securityService;
        private readonly IEmailService _emailService;

        public PersonService(IPersonRepository personRepository, IPersonJobTitleService personJobTitleService, IPersonStatutService personStatutService, IPersonRoleService personRoleService, ISecurityService securityService, IEmailService emailService)
        {
            _personRepository = personRepository;
            _personJobTitleService = personJobTitleService;
            _personStatutService = personStatutService;
            _personRoleService = personRoleService;
            _securityService = securityService;
            _emailService = emailService;
        }

        public async Task<Person> CreatePerson(CreatePersonDTO createPersonDTO)
        {

            var jobTitle = await _personJobTitleService.GetPersonJobTitleById(createPersonDTO.JobTitle_Id);
            if (jobTitle == null)
                throw new KeyNotFoundException("Job title not found.");

            var personExists = await _personRepository.GetPersonByEmail(createPersonDTO.Email);
            if (personExists != null)
                throw new InvalidOperationException("A person with this email already exists.");

            var personPhoneExists = await _personRepository.PersonPhoneNumberExists(createPersonDTO.PhoneNumber);
            if (personPhoneExists)
                throw new InvalidOperationException("A person with this phone number already exists.");

            var password = _securityService.GeneratePassword(12);
            var hashPassword = _securityService.HashPassword(password);

            var newPerson = new Person
            {
                Person_Email = createPersonDTO.Email,
                Person_FirstName = createPersonDTO.FirstName,
                Person_LastName = createPersonDTO.LastName,
                Person_PhoneNumber = createPersonDTO.PhoneNumber,
                Person_Description = $"Je suis {createPersonDTO.FirstName} {createPersonDTO.LastName}, nouveau chez Quacker!",
                Person_CreatedTimePerson = DateTime.Now,
                Person_ProfilPicturePath = "/Image/ProfilePicture/default.png",
                Person_Password = hashPassword,
                PersonJobTitle_Id = createPersonDTO.JobTitle_Id,
                PersonStatut_Id = 1,
                PersonRole_Id = 1
            };

            await _emailService.SendPasswordCreatedEmail(newPerson.Person_Email, password);
            await _personRepository.CreatePerson(newPerson);
            return newPerson;
        }

        public async Task<Person> CreatePersonTest(CreatePersonTestDTO createPersonTestDTO)
        {
            // Validate existence of related entities before creating a person
            var jobTitle = await _personJobTitleService.GetPersonJobTitleById(createPersonTestDTO.JobTitle_Id);
            if (jobTitle == null)
                throw new KeyNotFoundException("Job title not found.");

            var personExists = await _personRepository.GetPersonByEmail(createPersonTestDTO.Email);
            if (personExists != null)
                throw new InvalidOperationException("A person with this email already exists.");

            var personPhoneExists = await _personRepository.PersonPhoneNumberExists(createPersonTestDTO.PhoneNumber);
            if (personPhoneExists)
                throw new InvalidOperationException("A person with this phone number already exists.");

            var password = createPersonTestDTO.Password;
            var hashPassword = _securityService.HashPassword(password);

            var newPerson = new Person
            {
                Person_Email = createPersonTestDTO.Email,
                Person_FirstName = createPersonTestDTO.FirstName,
                Person_LastName = createPersonTestDTO.LastName,
                Person_PhoneNumber = createPersonTestDTO.PhoneNumber,
                Person_Description = $"Je suis {createPersonTestDTO.FirstName} {createPersonTestDTO.LastName}, nouveau chez Quacker!",
                Person_CreatedTimePerson = DateTime.Now,
                Person_ProfilPicturePath = "Path/To/Default/Image",
                Person_Password = hashPassword,
                PersonJobTitle_Id = createPersonTestDTO.JobTitle_Id,
                PersonStatut_Id = 1,
                PersonRole_Id = 1
            };

            await _personRepository.CreatePerson(newPerson);
            return newPerson;
        } 

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _personRepository.GetAllPersons() ?? new List<Person>();
        }

        public async Task<Person> GetPersonById(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person == null)
                throw new KeyNotFoundException($"No person found with ID {id}.");
            return person;
        }

        public async Task<Person> UpdatePerson(int id, UpdatePersonDTO updatePersonDTO)
        {
            var person = await GetPersonById(id);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with id {id} not found)");
            }

            if (updatePersonDTO.Description != null)
            {
                person.Person_Description = updatePersonDTO.Description;
            }

            if (updatePersonDTO.ProfilPicturePath != null)
            {
                person.Person_ProfilPicturePath = updatePersonDTO.ProfilPicturePath;
            }

            if (!string.IsNullOrWhiteSpace(updatePersonDTO.Password))
            {
                person.Person_Password = _securityService.HashPassword(updatePersonDTO.Password);
                person.Person_IsTemporaryPassword = false;
            }

            if (updatePersonDTO.StatutId.HasValue)
            {
                person.PersonStatut_Id = updatePersonDTO.StatutId.Value;
            }

            if (updatePersonDTO.JobTitleId.HasValue)
            {
                person.PersonJobTitle_Id = updatePersonDTO.JobTitleId.Value;
            }

            if (updatePersonDTO.RoleId.HasValue)
            {
                person.PersonRole_Id = updatePersonDTO.RoleId.Value;
            }

            if (!string.IsNullOrWhiteSpace(updatePersonDTO.PhoneNumber))
            {
                person.Person_PhoneNumber = updatePersonDTO.PhoneNumber;
            }

            await _personRepository.UpdatePerson(person);
            return person;
        }


        public async Task<bool> DeletePerson(int id)
        {
            var person = await GetPersonById(id);
            if (person == null)
                throw new KeyNotFoundException($"No person found with ID {id}.");

            await _personRepository.DeletePerson(id);
            return true;
        }

        public async Task<IEnumerable<Person>> GetPersonsByJobTitle(int jobTitleId)
        {
            var jobTitle = await _personJobTitleService.GetPersonJobTitleById(jobTitleId);
            if (jobTitle == null)
                throw new KeyNotFoundException($"Job title with ID {jobTitleId} does not exist.");

            return await _personRepository.GetPersonByJobTitle(jobTitleId);
        }

        public async Task<IEnumerable<Person>> GetPersonsByStatut(int statutId)
        {
            var statut = await _personStatutService.GetPersonStatutById(statutId);
            if (statut == null)
                throw new KeyNotFoundException($"Statut with ID {statutId} does not exist.");

            return await _personRepository.GetPersonByStatut(statutId);
        }

        public async Task<IEnumerable<Person>> GetPersonsByRole(int roleId)
        {
            var role = await _personRoleService.GetPersonRoleById(roleId);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {roleId} does not exist.");

            return await _personRepository.GetPersonByRole(roleId);
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            var person = await _personRepository.GetPersonByEmail(email);
            if (person == null)
                throw new KeyNotFoundException($"No person found with email {email}.");

            return person;
        }

        public async Task<string> UploadProfilePictureAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot/Image/ProfilePicture", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/image/ProfilePicture/{fileName}";
        }
    }
}
