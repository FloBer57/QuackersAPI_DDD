using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IPersonXChannelService
    {
        Task<IEnumerable<PersonXChannel>> GetAllAssociations();
        Task<PersonXChannel> GetAssociationById(int personId, int channelId);
        Task<PersonXChannel> CreateAssociation(CreatePersonXChannelDTO dto);
        Task<PersonXChannel> UpdateAssociation(int personId, int channelId, UpdatePersonXChannelDTO dto);
        Task<bool> DeleteAssociation(int personId, int channelId);
        Task<IEnumerable<Person>> GetPersonsByChannelId(int channelId);
        Task<IEnumerable<Channel>> GetChannelsByPersonId(int personId);
    }
}
