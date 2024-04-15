using QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelPersonRoleXPersonXChannelService : IChannelPersonRoleXPersonXChannelService
    {
        private readonly IChannelPersonRoleXPersonXChannelRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IChannelPersonRoleRepository _roleRepository;

        public ChannelPersonRoleXPersonXChannelService(
            IChannelPersonRoleXPersonXChannelRepository repository,
            IPersonRepository personRepository,
            IChannelRepository channelRepository,
            IChannelPersonRoleRepository roleRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
            _channelRepository = channelRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<ChannelPersonRoleXPersonXChannel>> GetAllAssociations()
        {
            return await _repository.GetAllAssociations();
        }

        public async Task<ChannelPersonRoleXPersonXChannel> GetAssociationByIds(int personId, int channelId)
        {
            return await _repository.GetAssociationByIds(personId, channelId)
                ?? throw new KeyNotFoundException("Association not found with the specified IDs.");
        }

        public async Task<ChannelPersonRoleXPersonXChannel> CreateAssociation(CreateChannelPersonRoleXPersonXChannelDTO dto)
        {
            var person = await _personRepository.GetPersonById(dto.PersonId);
            var channel = await _channelRepository.GetChannelById(dto.ChannelId);
            var role = await _roleRepository.GetChannelPersonRoleById(dto.ChannelPersonRole_Id);

            if (person == null || channel == null || role == null)
                throw new KeyNotFoundException("One or more entities not found.");

            var association = new ChannelPersonRoleXPersonXChannel
            {
                Person_Id = dto.PersonId,
                Channel_Id = dto.ChannelId,
                ChannelPersonRole_Id = dto.ChannelPersonRole_Id,
                ChannelPersonRoleXpersonXchannelAffectDate = DateTime.Now,
            };

            return await _repository.CreateAssociation(association);
        }

        public async Task<ChannelPersonRoleXPersonXChannel> UpdateAssociation(int personId, int channelId, UpdateChannelPersonRoleXPersonXChannelDTO dto)
        {
            var association = await _repository.GetAssociationByIds(personId, channelId);
            if (association == null)
                throw new KeyNotFoundException("Association not found with the specified IDs.");

            var role = await _roleRepository.GetChannelPersonRoleById(dto.ChannelPersonRoleId);
            if (role == null)
                throw new KeyNotFoundException("ChannelPersonRole not found with the specified ID.");

            association.ChannelPersonRole = role;

            return await _repository.UpdateAssociation(association);
        }

        public async Task<bool> DeleteAssociation(int personId, int channelId)
        {
            return await _repository.DeleteAssociation(personId, channelId);
        }
    }
}
