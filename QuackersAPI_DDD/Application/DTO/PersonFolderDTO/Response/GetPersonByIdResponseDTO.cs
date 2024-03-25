using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response
{
    public class GetPersonByIdResponseDTO
    {
        public PersonDTO Person { get; set; }
        public string Message { get; set; }

        public GetPersonByIdResponseDTO(PersonDTO person)
        {
            Person = person;
            Message = person != null
                ? $"L'utilisateur {person.FirstName} {person.LastName} a été récupéré avec succès."
                : "Utilisateur non trouvé.";
        }
    }
}