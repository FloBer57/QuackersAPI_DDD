using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IChannelTypeService
    {
        Task<IEnumerable<ChannelType>> GetAllChannelTypes();
        Task<ChannelType> GetChannelTypeById(int id);
        Task<ChannelType> CreateChannelType(ChannelType channelType);
        Task<ChannelType> UpdateChannelType(int id, ChannelType channelType);
        Task<bool> DeleteChannelType(int id);
    }
}
