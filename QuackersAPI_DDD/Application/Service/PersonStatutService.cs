using QuackersAPI_DDD.API.DTO.PersonStatutDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonStatutService : IPersonStatutService
    {
        private readonly IPersonStatutRepository _personStatutRepository;

        public PersonStatutService(IPersonStatutRepository personStatutRepository)
        {
            _personStatutRepository = personStatutRepository ?? throw new ArgumentNullException(nameof(personStatutRepository));
        }

        public async Task<IEnumerable<PersonStatut>> GetAllPersonStatuts()
        {
            var statuts = await _personStatutRepository.GetAllPersonStatuts();
            return statuts ?? new List<PersonStatut>();
        }

        public async Task<PersonStatut> GetPersonStatutById(int id)
        {
            var statut = await _personStatutRepository.GetPersonStatutById(id);
            if (statut == null)
                throw new KeyNotFoundException($"Person status with ID {id} not found.");
            return statut;
        }

        public async Task<PersonStatut> CreatePersonStatut(CreatePersonStatutDTO personStatutDto)
        {
            if (await _personStatutRepository.PersonStatutNameExists(personStatutDto.PersonStatut_Name))
                throw new InvalidOperationException($"A person status with the name '{personStatutDto.PersonStatut_Name}' already exists.");

            var personStatut = new PersonStatut
            {
                PersonStatut_Name = personStatutDto.PersonStatut_Name
            };
            return await _personStatutRepository.CreatePersonStatut(personStatut);
        }

        public async Task<PersonStatut> UpdatePersonStatut(int id, UpdatePersonStatutDTO personStatutDto)
        {
            var existingStatut = await _personStatutRepository.GetPersonStatutById(id);
            if (existingStatut == null)
                throw new KeyNotFoundException($"Person status with ID {id} not found.");

            if (await _personStatutRepository.PersonStatutNameExists(personStatutDto.PersonStatut_Name))
                throw new InvalidOperationException($"A person status with the name '{personStatutDto.PersonStatut_Name}' already exists.");

            existingStatut.PersonStatut_Name = personStatutDto.PersonStatut_Name;
            return await _personStatutRepository.UpdatePersonStatut(id, existingStatut);
        }

        public async Task<bool> DeletePersonStatut(int id)
        {
            var statut = await _personStatutRepository.GetPersonStatutById(id);
            if (statut == null)
                throw new KeyNotFoundException($"Person status with ID {id} not found.");

            await _personStatutRepository.DeletePersonStatut(id);
            return true;
        }
    }
}
