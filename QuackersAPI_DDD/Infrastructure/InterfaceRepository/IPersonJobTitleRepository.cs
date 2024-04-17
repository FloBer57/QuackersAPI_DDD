using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonJobTitleRepository
    {
        Task<PersonJobTitle> CreatePersonJobTitle(PersonJobTitle personJobTitle);
        Task<IEnumerable<PersonJobTitle>> GetAllPersonJobTitle();
        Task<PersonJobTitle> GetPersonJobTitleById(int id);
        Task<PersonJobTitle> UpdatePersonJobTitle(PersonJobTitle personJobTitle);
        Task DeletePersonJobTitle(PersonJobTitle personJobTitle);
        Task<bool> PersonJobTitleNameExists(string name);
    }
}
