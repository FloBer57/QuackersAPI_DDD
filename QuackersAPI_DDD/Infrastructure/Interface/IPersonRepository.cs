using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Interface
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonById(int id);
        Task<IEnumerable<Person>> GetAllPerson();
        Task CreatePerson(Person person);
        Task UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
