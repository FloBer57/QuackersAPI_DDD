using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;
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

        public async Task<ChannelType> CreateChannelType(CreateChannelTypeDTO dto)
        {
            var channelType = new ChannelType { ChannelType_Name = dto.ChannelType_Name };
            if (await _channelTypeRepository.ChannelTypeNameExists(dto.ChannelType_Name))
            {
                throw new InvalidOperationException($"A channel type with the same name '{dto.ChannelType_Name} exist");
            }
            return await _channelTypeRepository.CreateChannelType(channelType);
        }

        public async Task<ChannelType> UpdateChannelType(int id, UpdateChannelTypeDTO updateChannelTypeDTO)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                throw new InvalidOperationException($"ChannelType with id {id} not found.");
            }

            if (await _channelTypeRepository.ChannelTypeNameExists(updateChannelTypeDTO.ChannelType_Name))
            {
                throw new InvalidOperationException($"A channel type with the same name {updateChannelTypeDTO.ChannelType_Name} exist");
            }

            channelType.ChannelType_Name = updateChannelTypeDTO.ChannelType_Name;

            return await _channelTypeRepository.UpdateChannelType(channelType);
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
