using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IPersonXMessageRepository
    {
        Task<IEnumerable<PersonXMessage>> GetAllAssociations();
        Task<PersonXMessage> GetAssociationById(int personId, int messageId);
        Task<PersonXMessage> CreateAssociation(PersonXMessage association);
        Task<PersonXMessage> UpdateAssociation(PersonXMessage association);
        Task<bool> DeleteAssociation(int personId, int messageId);
    }
}
