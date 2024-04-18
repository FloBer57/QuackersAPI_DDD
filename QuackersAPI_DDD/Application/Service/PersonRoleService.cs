using QuackersAPI_DDD.API.DTO.PersonJobTitleDTO;
using QuackersAPI_DDD.API.DTO.PersonRoleDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

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
            if (await _personRoleRepository.PersonRoleNameExists(createPersonRoleDTO.PersonRole_Name))
            {
                throw new InvalidOperationException($"A person job title with the name '{createPersonRoleDTO.PersonRole_Name}' already exists.");
            }

            var personRole = new PersonRole { PersonRole_Name = createPersonRoleDTO.PersonRole_Name };
            return await _personRoleRepository.CreatePersonRole(personRole);
        }

        public async Task<IEnumerable<PersonRole>> GetAllPersonRoles()
        {
            var personRoles =  await _personRoleRepository.GetAllPersonRoles();
            return personRoles ?? new List<PersonRole>();

        }

        public async Task<PersonRole> GetPersonRoleById(int id)
        {
            var personRole = await _personRoleRepository.GetPersonRoleById(id);
            if (personRole == null)
            {
                throw new KeyNotFoundException($"Person role with id {id} not found.");
            }
            return personRole;
        }

        public async Task<PersonRole> UpdatePersonRole(int id, UpdatePersonRoleDTO updatePersonRoleDTO)
        {
            var personRole = await _personRoleRepository.GetPersonRoleById(id);
            if (personRole == null)
            {
                throw new KeyNotFoundException($"Person role with id {id} not found.");
            }
            if (await _personRoleRepository.PersonRoleNameExists(updatePersonRoleDTO.PersonRole_Name))
            {
                throw new InvalidOperationException($"A person job title with the name '{updatePersonRoleDTO.PersonRole_Name}' already exists.");
            }

            personRole.PersonRole_Name = updatePersonRoleDTO.PersonRole_Name;
            return await _personRoleRepository.UpdatePersonRole(personRole);
        }

        public async Task<bool> DeletePersonRole(int id)
        {
            var personRole = await _personRoleRepository.GetPersonRoleById(id);
            if (personRole == null)
            {
                throw new KeyNotFoundException($"Person role with id {id} not found.");
            }

            await _personRoleRepository.DeletePersonRole(id);
            return true;
        }
    }
}
