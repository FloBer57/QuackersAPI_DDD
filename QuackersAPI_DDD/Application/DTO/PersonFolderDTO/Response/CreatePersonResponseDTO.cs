using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response
{
    public class CreatePersonResponseDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public CreatePersonResponseDTO(PersonDTO person)
        {
            Message = $"L'utilisateur {person.FirstName} {person.LastName} a été créé.";
        }
    }
}
