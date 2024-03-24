using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.Response
{
    public class GetAllPersonResponseDTO
    {
        public IEnumerable<PersonDTO> Persons { get; set; }
        public string Message { get; set; }
        public int NumberOfPerson {  get; set; }

        public GetAllPersonResponseDTO(IEnumerable<PersonDTO> persons)
        {
            Persons = persons;
            foreach (var item in persons)
            {
                NumberOfPerson++;
            }
            Message = $"Vous avez récupéré {NumberOfPerson} Personnes.";
            
        }
    }
}