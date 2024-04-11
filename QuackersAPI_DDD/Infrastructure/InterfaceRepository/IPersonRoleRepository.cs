using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonRoleRepository
    {
        Task<PersonRole> CreatePersonRole(PersonRole personRole);
        Task<IEnumerable<PersonRole>> GetAllPersonRoles();
        Task<PersonRole> GetPersonRoleById(int id);
        Task<PersonRole> UpdatePersonRole(PersonRole personRole);
        Task DeletePersonRole(int id);
    }
}
