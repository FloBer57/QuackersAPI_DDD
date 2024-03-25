using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response
{
    public class UpdatePersonByIdResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UpdatePersonByIdResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}

