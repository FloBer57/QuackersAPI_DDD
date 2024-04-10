namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Domain.Model;

    public interface IPersonRepository
    {
        Task<Person> CreatePerson(Person person);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person> GetPersonById(int id);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
