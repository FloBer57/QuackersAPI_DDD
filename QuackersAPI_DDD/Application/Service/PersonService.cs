using System.Collections.Generic;
using System.Threading.Tasks;
using QuackersAPI_DDD.Application.DTO;
using QuackersAPI_DDD.Application.DTO.Request;
using QuackersAPI_DDD.Application.DTO.Response;
using QuackersAPI_DDD.Application.Interface;

namespace QuackersAPI_DDD.Application.Service // Assurez-vous que le namespace correspond à votre structure
{
    public class PersonService : IPersonService
    {
        public async Task<CreatePersonRequestDTO> CreatePerson(CreatePersonRequestDTO personDto)
        {
            throw new NotImplementedException();
        } 

        public async Task DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPersonByIdResponseDTO> GetPersonById(int id)
        {
            throw new NotImplementedException();
            // La logique pour obtenir la personne par son ID devrait être ici
        }

        Task<CreatePersonResponseDTO> IPersonService.CreatePerson(CreatePersonRequestDTO personDto)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GetAllPersonsRequestDTO>> IPersonService.GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
