using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.Response
{
    public class GetPersonByIdResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }

        public GetPersonByIdResponseDTO(Person person)
        {
            FirstName = person.Person_FirstName;
            LastName = person.Person_LastName;
            Message = $"L'utilisateur {FirstName} {LastName} a été récupéré avec succès.";
        }
    }
}
