using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXChannelService : IPersonXChannelService
    {
        private readonly IPersonXChannelRepository _repository;

        public PersonXChannelService(IPersonXChannelRepository repository)
        {
            _repository = repository;
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
    }
}
