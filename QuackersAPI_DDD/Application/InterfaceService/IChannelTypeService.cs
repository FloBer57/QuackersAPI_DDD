using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IChannelTypeService
    {
        Task<IEnumerable<ChannelType>> GetAllChannelTypes();
        Task<ChannelType> GetChannelTypeById(int id);
        Task<ChannelType> CreateChannelType(CreateChannelTypeDTO channelType);
        Task<ChannelType> UpdateChannelType(int id, UpdateChannelTypeDTO channelType);
        Task<bool> DeleteChannelType(int id);
    }
}
