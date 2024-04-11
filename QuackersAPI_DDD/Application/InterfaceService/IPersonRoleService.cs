using QuackersAPI_DDD.API.DTO.PersonRoleDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IPersonRoleService
    {
        Task<PersonRole> CreatePersonRole(CreatePersonRoleDTO createPersonRoleDTO);
        Task<IEnumerable<PersonRole>> GetAllPersonRoles();
        Task<PersonRole> GetPersonRoleById(int id);
        Task<PersonRole> UpdatePersonRole(int id, UpdatePersonRoleDTO updatePersonRoleDTO);
        Task<bool> DeletePersonRole(int id);
    }
}
