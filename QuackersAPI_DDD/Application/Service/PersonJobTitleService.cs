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
            if (await _personJobTitleRepository.PersonJobTitleNameExists(createPersonJobTitleDTO.JobTitle_Name))
            {
                throw new InvalidOperationException($"A person job title with the name '{createPersonJobTitleDTO.JobTitle_Name}' already exists.");
            }

            var personJobTitle = new PersonJobTitle { PersonJobTitle_Name = createPersonJobTitleDTO.JobTitle_Name };
            return await _personJobTitleRepository.CreatePersonJobTitle(personJobTitle);
        }

        public async Task<IEnumerable<PersonJobTitle>> GetAllPersonJobTitle()
        {
            var personJobTitles = await _personJobTitleRepository.GetAllPersonJobTitle();
            return personJobTitles ?? new List<PersonJobTitle>();
        }

        public async Task<PersonJobTitle> GetPersonJobTitleById(int id)
        {
            var personJobTitle = await _personJobTitleRepository.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                throw new KeyNotFoundException($"Person job title with id {id} not found.");
            }
            return personJobTitle;
        }

        public async Task<PersonJobTitle> UpdatePersonJobTitle(int id, UpdatePersonJobTitleDTO updatePersonJobTitleDTO)
        {
            var personJobTitle = await _personJobTitleRepository.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                throw new KeyNotFoundException($"Person job title with id {id} not found.");
            }
            if (await _personJobTitleRepository.PersonJobTitleNameExists(updatePersonJobTitleDTO.JobTitle_Name))
            {
                throw new InvalidOperationException($"A person job title with the name '{updatePersonJobTitleDTO.JobTitle_Name}' already exists.");
            }

            personJobTitle.PersonJobTitle_Name = updatePersonJobTitleDTO.JobTitle_Name;
            return await _personJobTitleRepository.UpdatePersonJobTitle(personJobTitle);
        }

        public async Task<bool> DeletePersonJobTitle(int id)
        {
            var personJobTitle = await _personJobTitleRepository.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                throw new KeyNotFoundException($"Person job title with id {id} not found.");
            }

            await _personJobTitleRepository.DeletePersonJobTitle(personJobTitle);
            return true;
        }
    }
}
