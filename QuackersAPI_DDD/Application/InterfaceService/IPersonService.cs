namespace QuackersAPI_DDD.Application.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.API.DTO.PersonDTO;
    using QuackersAPI_DDD.Domain.Model;

    public interface IPersonService
    {
        Task<Person> CreatePerson(CreatePersonDTO createPersonDTO);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<IEnumerable<Person>> GetPersonsByJobTitle(int jobTitleId);
        Task<IEnumerable<Person>> GetPersonsByStatut(int statutId);
        Task<IEnumerable<Person>> GetPersonsByRole(int roleId);
        Task<Person> GetPersonByEmail(string email);
        Task<Person> GetPersonById(int id);
        Task<Person> UpdatePerson(int id, UpdatePersonDTO person);
        Task<bool> DeletePerson(int id);
    }
}
