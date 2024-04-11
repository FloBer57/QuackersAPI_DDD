using QuackersAPI_DDD.API.DTO.PersonJobTitleDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonJobTitleService : IPersonJobTitleService
    {
        private readonly IPersonJobTitleRepository _personJobTitleRepository;

        public PersonJobTitleService(IPersonJobTitleRepository personJobTitleRepository)
        {
            _personJobTitleRepository = personJobTitleRepository ?? throw new ArgumentNullException(nameof(personJobTitleRepository));
        }

        public async Task<PersonJobTitle> CreatePersonJobTitle(CreatePersonJobTitleDTO createPersonJobTitleDTO)
        {
            var personJobTitle = new PersonJobTitle { PersonJobTitle_Name = createPersonJobTitleDTO.JobTitle_Name };
            return await _personJobTitleRepository.CreatePersonJobTitle(personJobTitle);
        }

        public async Task<IEnumerable<PersonJobTitle>> GetAllPersonJobTitle()
        {
            return await _personJobTitleRepository.GetAllPersonJobTitle();
        }

        public async Task<PersonJobTitle> GetPersonJobTitleById(int id)
        {
            return await _personJobTitleRepository.GetPersonJobTitleById(id);
        }

        public async Task<PersonJobTitle> UpdatePersonJobTitle(int id, UpdatePersonJobTitleDTO updatePersonJobTitleDTO)
        {
            var personJobTitle = await _personJobTitleRepository.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                throw new InvalidOperationException($"Person job title with id {id} not found.");
            }

            personJobTitle.PersonJobTitle_Name = updatePersonJobTitleDTO.JobTitle_Name;

            return await _personJobTitleRepository.UpdatePersonJobTitle(personJobTitle);
        }

        public async Task<bool> DeletePersonJobTitle(int id)
        {
            var personJobTitle = await _personJobTitleRepository.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                return false;
            }

            await _personJobTitleRepository.DeletePersonJobTitle(personJobTitle);
            return true;
        }
    }
}
