using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonXChannelRepository
    {
        Task<IEnumerable<PersonXChannel>> GetAllAssociations();
        Task<PersonXChannel> GetAssociationById(int personId, int channelId);
        Task<PersonXChannel> CreateAssociation(PersonXChannel association);
        Task<PersonXChannel> UpdateAssociation(PersonXChannel association);
        Task<bool> DeleteAssociation(int personId, int channelId);
    }
}
