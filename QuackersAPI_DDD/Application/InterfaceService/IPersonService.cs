namespace QuackersAPI_DDD.Application.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request;
    using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response;
    using QuackersAPI_DDD.Domain.Model;

    public interface IPersonService
    {
        Task<Person> CreatePerson(Person person);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person> GetPersonById(int id);
        Task<Person> UpdatePerson(int id, Person person);
        Task<bool> DeletePerson(int id);
    }
}
