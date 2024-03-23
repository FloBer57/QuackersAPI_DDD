using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.Response
{
    public class DeletePersonByIdResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }

        public DeletePersonByIdResponseDTO(Person person) 
        {
            FirstName = person.Person_FirstName;
            LastName = person.Person_LastName;
            Message = $"L'utilisateur {FirstName} {LastName} à été supprimé avec succès.";
        }    
    }
}
