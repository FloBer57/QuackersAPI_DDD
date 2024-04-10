using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelTypeService : IChannelTypeService
    {
        private readonly IChannelTypeRepository _channelTypeRepository;

        public ChannelTypeService(IChannelTypeRepository channelTypeRepository)
        {
            _channelTypeRepository = channelTypeRepository;
        }

        public async Task<IEnumerable<ChannelType>> GetAllChannelTypes()
        {
            return await _channelTypeRepository.GetAllChannelTypes();
        }

        public async Task<ChannelType> GetChannelTypeById(int id)
        {
            return await _channelTypeRepository.GetChannelTypeById(id);
        }

        public async Task<ChannelType> CreateChannelType(ChannelType channelType)
        {
            return await _channelTypeRepository.CreateChannelType(channelType);
        }

        public async Task<ChannelType> UpdateChannelType(int id, ChannelType updatedChannelType)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                throw new InvalidOperationException($"ChannelType with id {id} not found.");
            }

            // Assume this method updates the channel type with the new values
            return await _channelTypeRepository.UpdateChannelType(updatedChannelType);
        }

        public async Task<bool> DeleteChannelType(int id)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                return false;
            }

            await _channelTypeRepository.DeleteChannelType(id);
            return true;
        }
    }

}
