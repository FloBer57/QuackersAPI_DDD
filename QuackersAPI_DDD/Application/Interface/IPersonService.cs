
namespace QuackersAPI_DDD.Application.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Application.DTO.Request; 
    using QuackersAPI_DDD.Application.DTO.Response; 

    public interface IPersonService
    {
        Task<GetPersonByIdResponseDTO> GetPersonById(int id);
        Task<IEnumerable<GetAllPersonsRequestDTO>> GetAllPersons();
        Task<CreatePersonResponseDTO> CreatePerson(CreatePersonRequestDTO personDto);
    }
}
