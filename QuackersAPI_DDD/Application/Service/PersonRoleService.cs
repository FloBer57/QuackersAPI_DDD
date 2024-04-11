using QuackersAPI_DDD.API.DTO.PersonRoleDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonRoleService : IPersonRoleService
    {
        private readonly IPersonRoleRepository _personRoleRepository;

        public PersonRoleService(IPersonRoleRepository personRoleRepository)
        {
            _personRoleRepository = personRoleRepository ?? throw new ArgumentNullException(nameof(personRoleRepository));
        }

        public async Task<PersonRole> CreatePersonRole(CreatePersonRoleDTO createPersonRoleDTO)
        {
            var personRole = new PersonRole { PersonRole_Name = createPersonRoleDTO.PersonRole_Name };
            return await _personRoleRepository.CreatePersonRole(personRole);
        }

        public async Task<IEnumerable<PersonRole>> GetAllPersonRoles()
        {
            return await _personRoleRepository.GetAllPersonRoles();
        }

        public async Task<PersonRole> GetPersonRoleById(int id)
        {
            return await _personRoleRepository.GetPersonRoleById(id);
        }

        public async Task<PersonRole> UpdatePersonRole(int id, UpdatePersonRoleDTO updatePersonRoleDTO)
        {
            var personRole = await _personRoleRepository.GetPersonRoleById(id);
            if (personRole == null)
            {
                throw new InvalidOperationException($"PersonRole with id {id} not found.");
            }

            personRole.PersonRole_Name = updatePersonRoleDTO.PersonRole_Name;

            return await _personRoleRepository.UpdatePersonRole(personRole);
        }

        public async Task<bool> DeletePersonRole(int id)
        {
            var personRole = await _personRoleRepository.GetPersonRoleById(id);
            if (personRole == null)
            {
                return false;
            }

            await _personRoleRepository.DeletePersonRole(id);
            return true;
        }
    }
}
