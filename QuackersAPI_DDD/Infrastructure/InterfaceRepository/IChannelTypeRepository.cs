using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IChannelTypeRepository
    {
        Task<IEnumerable<ChannelType>> GetAllChannelTypes();
        Task<ChannelType> GetChannelTypeById(int id);
        Task<ChannelType> CreateChannelType(ChannelType channelType);
        Task<ChannelType> UpdateChannelType(ChannelType channelType);
        Task DeleteChannelType(int id);
    }
}
