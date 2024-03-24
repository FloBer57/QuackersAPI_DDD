namespace QuackersAPI_DDD.Infrastructure.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Domain.Model;

    public interface IPersonRepository
    {
        Task CreatePerson(Person person);
        Task<Person> GetPersonById(int id);
        Task<IEnumerable<Person>> GetAllPerson();
        Task UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
