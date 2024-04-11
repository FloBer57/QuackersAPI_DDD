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
        Task<IEnumerable<Person>> GetPersonByJobTitle(int jobTitleId);
        Task<IEnumerable<Person>> GetPersonByStatut(int statutId);
        Task<IEnumerable<Person>> GetPersonByRole(int roleId);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
