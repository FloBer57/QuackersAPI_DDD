using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IChannelPersonRoleXPersonXChannelService
    {
        Task<IEnumerable<ChannelPersonRoleXPersonXChannel>> GetAllAssociations();
        Task<ChannelPersonRoleXPersonXChannel> GetAssociationByIds(int personId, int channelId);
        Task<ChannelPersonRoleXPersonXChannel> CreateAssociation(CreateChannelPersonRoleXPersonXChannelDTO dto);
        Task<ChannelPersonRoleXPersonXChannel> UpdateAssociation(int personId, int channelId, UpdateChannelPersonRoleXPersonXChannelDTO dto);
        Task<bool> DeleteAssociation(int personId, int channelId);
        Task<IEnumerable<ChannelPersonRole>> GetRolesByPersonInChannels(int personId);
        Task<IEnumerable<Person>> GetPersonsByRoleInChannel(int channelId, int roleId);
    }
}