using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.Response
{
    public class GetAllPersonsResponseDTO
    {
        public IEnumerable<PersonDTO> Persons { get; set; }

        public GetAllPersonsResponseDTO(IEnumerable<PersonDTO> persons)
        {
            Persons = persons;
        }
    }
}