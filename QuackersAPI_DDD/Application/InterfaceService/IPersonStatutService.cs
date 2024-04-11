using QuackersAPI_DDD.API.DTO.PersonStatutDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IPersonStatutService
    {
        Task<IEnumerable<PersonStatut>> GetAllPersonStatuts();
        Task<PersonStatut> GetPersonStatutById(int id);
        Task<PersonStatut> CreatePersonStatut(CreatePersonStatutDTO personStatut);
        Task<PersonStatut> UpdatePersonStatut(int id, UpdatePersonStatutDTO personStatut);
        Task<bool> DeletePersonStatut(int id);
    }
}
