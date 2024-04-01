namespace QuackersAPI_DDD.Application.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request;
    using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response;

    public interface IPersonService
    {
        Task<CreatePersonResponseDTO> CreatePerson(CreatePersonRequestDTO personDto);
        Task<GetAllPersonResponseDTO> GetAllPersons();
        Task<GetPersonByIdResponseDTO> GetPersonById(int id);
        Task<UpdatePersonByIdResponseDTO> UpdatePassword(int id, string newPassword);
        Task<UpdatePersonByIdResponseDTO> UpdatePhoneNumber(int id, string newPhoneNumber);
        Task<DeletePersonByIdResponseDTO> DeletePerson(int id);
    }
}
