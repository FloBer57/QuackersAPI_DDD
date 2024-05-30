using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonStatutRepository
    {
        Task<IEnumerable<PersonStatut>> GetAllPersonStatuts();
        Task<PersonStatut> GetPersonStatutById(int id);
        Task<PersonStatut> CreatePersonStatut(PersonStatut personStatut);
        Task<PersonStatut> UpdatePersonStatut(int id, PersonStatut personStatut);
        Task<bool> DeletePersonStatut(int id);
        Task<bool> PersonStatutNameExists(string name);

    }
}
