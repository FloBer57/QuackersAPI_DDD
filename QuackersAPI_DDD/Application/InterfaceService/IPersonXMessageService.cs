using QuackersAPI_DDD.API.DTO.PersonXMessageDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IPersonXMessageService
    {
        Task<IEnumerable<PersonXMessage>> GetAllAssociations();
        Task<PersonXMessage> GetAssociationById(int personId, int messageId);
        Task<PersonXMessage> CreateAssociation(CreatePersonXMessageDTO dto);
        Task<PersonXMessage> UpdateAssociation(int personId, int messageId, UpdatePersonXMessageDTO dto);
        Task<bool> DeleteAssociation(int personId, int messageId);
    }
}
