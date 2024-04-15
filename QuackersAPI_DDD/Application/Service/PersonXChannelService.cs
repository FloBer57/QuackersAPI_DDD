using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXChannelService : IPersonXChannelService
    {
        private readonly IPersonXChannelRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IChannelRepository _channelRepository;

        public PersonXChannelService(IPersonXChannelRepository repository, IPersonRepository personRepository, IChannelRepository channelRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IEnumerable<PersonXChannel>> GetAllAssociations()
        {
            return await _repository.GetAllAssociations();
        }

        public async Task<PersonXChannel> GetAssociationById(int personId, int channelId)
        {
            return await _repository.GetAssociationById(personId, channelId);
        }

        public async Task<PersonXChannel> CreateAssociation(CreatePersonXChannelDTO dto)
        {
            var newAssociation = new PersonXChannel
            {
                Person_Id = dto.PersonId,
                Channel_Id = dto.ChannelId,
                PersonXchannelSignInDate = DateTime.Now,
            };

            return await _repository.CreateAssociation(newAssociation);
        }

        public async Task<PersonXChannel> UpdateAssociation(int personId, int channelId, UpdatePersonXChannelDTO dto)
        {
            var association = await _repository.GetAssociationById(personId, channelId);
            if (association == null)
                throw new KeyNotFoundException("Association not found.");

            association.PersonXchannelSignInDate = dto.SignInDate ?? association.PersonXchannelSignInDate;
            return await _repository.UpdateAssociation(association);
        }

        public async Task<bool> DeleteAssociation(int personId, int channelId)
        {
            return await _repository.DeleteAssociation(personId, channelId);
        }

        public async Task AddPersonToChannel(int personId, int channelId)
        {
            var person = await _personRepository.GetPersonById(personId);
            var channel = await _channelRepository.GetChannelById(channelId);
            if (person == null || channel == null)
            {
                throw new ArgumentException("Person or Channel not found.");
            }

            var existingAssociation = await _repository.GetAssociationById(personId, channelId);
            if (existingAssociation != null)
            {
                throw new InvalidOperationException("This association already exists.");
            }

            await _repository.AddPersonToChannel(personId, channelId); 
        }

    }
}
