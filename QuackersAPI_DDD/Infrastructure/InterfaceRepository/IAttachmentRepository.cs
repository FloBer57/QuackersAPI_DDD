using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IAttachmentRepository
    {
        Task<Attachment> GetAttachmentById(int id);
        Task<IEnumerable<Attachment>> GetAttachmentsByMessageId(int messageId);
        Task<Attachment> CreateAttachment(Attachment attachment);
        Task<bool> DeleteAttachment(int id);
        Task<bool> AttachmentNameExists(string attachmentName);
    }
}
