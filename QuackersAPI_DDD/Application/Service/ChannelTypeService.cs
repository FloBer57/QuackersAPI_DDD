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
            var channelType = await _channelTypeRepository.GetAllChannelTypes();
            return channelType ?? new List<ChannelType>();
        }

        public async Task<ChannelType> GetChannelTypeById(int id)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                throw new KeyNotFoundException($"ChannelType with id {id} not found.");
            }
            return channelType;
        }

        public async Task<ChannelType> CreateChannelType(CreateChannelTypeDTO dto)
        {
            if (await _channelTypeRepository.ChannelTypeNameExists(dto.ChannelType_Name))
            {
                throw new InvalidOperationException($"A channel type with the same name '{dto.ChannelType_Name}' already exists.");
            }

            var channelType = new ChannelType { ChannelType_Name = dto.ChannelType_Name };
            return await _channelTypeRepository.CreateChannelType(channelType);
        }

        public async Task<ChannelType> UpdateChannelType(int id, UpdateChannelTypeDTO dto)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                throw new KeyNotFoundException($"ChannelType with id {id} not found.");
            }

            if (await _channelTypeRepository.ChannelTypeNameExists(dto.ChannelType_Name) && dto.ChannelType_Name != channelType.ChannelType_Name)
            {
                throw new InvalidOperationException($"A channel type with the same name '{dto.ChannelType_Name}' already exists.");
            }

            channelType.ChannelType_Name = dto.ChannelType_Name;
            return await _channelTypeRepository.UpdateChannelType(channelType);
        }

        public async Task<bool> DeleteChannelType(int id)
        {
            var channelType = await _channelTypeRepository.GetChannelTypeById(id);
            if (channelType == null)
            {
                throw new KeyNotFoundException($"ChannelType with id {id} not found.");
            }

            await _channelTypeRepository.DeleteChannelType(id);
            return true;
        }

    }

}
