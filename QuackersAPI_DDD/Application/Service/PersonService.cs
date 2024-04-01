using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuackersAPI_DDD.Application.DTO;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreatePersonResponseDTO> CreatePerson(CreatePersonRequestDTO personDto)
        {
            var person = new Person
            {
                Person_FirstName = personDto.Firstname,
                Person_LastName = personDto.LastName,
                Person_Email = personDto.Email,
                Person_PhoneNumber = personDto.PhoneNumber,
            };

            await _repository.CreatePerson(person);
            return new CreatePersonResponseDTO(new PersonDTO(person));
        }

        public async Task<GetAllPersonResponseDTO> GetAllPersons()
        {
            var persons = await _repository.GetAllPerson();
            var personDtos = persons.Select(p => new PersonDTO(p)).ToList();
            return new GetAllPersonResponseDTO(personDtos);
        }

        public async Task<GetPersonByIdResponseDTO> GetPersonById(int id)
        {
            var person = await _repository.GetPersonById(id);
            if (person == null)
            {
                return null;
            }

            var personDto = new PersonDTO(person);
            return new GetPersonByIdResponseDTO(personDto);
        }

        public async Task<UpdatePersonByIdResponseDTO> UpdatePassword(int id, string newPassword)
        {
            var person = await _repository.GetPersonById(id);
            if (person != null)
            {
                person.Person_Password = SecurityService.HashPassword(newPassword);
                await _repository.UpdatePerson(person);
                return new UpdatePersonByIdResponseDTO(true, $"Le mot de passe de {person.Person_FirstName} {person.Person_LastName} a bien été modifié.");
            }
            return new UpdatePersonByIdResponseDTO(false, "Utilisateur non trouvé.");
        }

        public async Task<UpdatePersonByIdResponseDTO> UpdatePhoneNumber(int id, string newPhoneNumber)
        {
            var person = await _repository.GetPersonById(id);
            if (person != null)
            {
                person.Person_PhoneNumber = newPhoneNumber;
                await _repository.UpdatePerson(person);
                return new UpdatePersonByIdResponseDTO(true, $"Le numéro de téléphone de {person.Person_FirstName} {person.Person_LastName} a bien été modifié.");
            }
            return new UpdatePersonByIdResponseDTO(false, "Utilisateur non trouvé.");
        }

        public async Task<DeletePersonByIdResponseDTO> DeletePerson(int id)
        {
            var person = await _repository.GetPersonById(id);
            if (person == null)
            {
                return new DeletePersonByIdResponseDTO(false, "Utilisateur non trouvé.");
            }

            await _repository.DeletePerson(id);
            return new DeletePersonByIdResponseDTO(true, $"L'utilisateur {person.Person_FirstName} {person.Person_LastName} a été supprimé avec succès");
        }
    }
}
