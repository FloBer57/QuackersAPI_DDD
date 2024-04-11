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
            return await _personStatutRepository.GetAllPersonStatuts();
        }

        public async Task<PersonStatut> GetPersonStatutById(int id)
        {
            return await _personStatutRepository.GetPersonStatutById(id);
        }

        public async Task<PersonStatut> CreatePersonStatut(CreatePersonStatutDTO personStatutDto)
        {
            var personStatut = new PersonStatut
            {
                PersonStatut_Name = personStatutDto.PersonStatut_Name
            };
            return await _personStatutRepository.CreatePersonStatut(personStatut);
        }

        public async Task<PersonStatut> UpdatePersonStatut(int id, UpdatePersonStatutDTO personStatutDto)
        {
            var personStatut = new PersonStatut
            {
                PersonStatut_Name = personStatutDto.PersonStatut_Name
            };
            return await _personStatutRepository.UpdatePersonStatut(id, personStatut);
        }

        public async Task<bool> DeletePersonStatut(int id)
        {
            return await _personStatutRepository.DeletePersonStatut(id);
        }
    }
}
