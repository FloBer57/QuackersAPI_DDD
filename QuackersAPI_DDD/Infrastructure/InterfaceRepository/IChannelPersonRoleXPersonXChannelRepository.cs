﻿using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IChannelPersonRoleXPersonXChannelRepository
    {
        Task<IEnumerable<ChannelPersonRoleXPersonXChannel>> GetAllAssociations();
        Task<ChannelPersonRoleXPersonXChannel> GetAssociationByIds(int personId, int channelId);
        Task<ChannelPersonRoleXPersonXChannel> CreateAssociation(ChannelPersonRoleXPersonXChannel association);
        Task<ChannelPersonRoleXPersonXChannel> UpdateAssociation(ChannelPersonRoleXPersonXChannel association);
        Task<bool> DeleteAssociation(int personId, int channelId);
    }
}
