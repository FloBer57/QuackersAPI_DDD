using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAllAttachments();
        Task<Attachment> GetAttachmentById(int id);
        Task<Attachment> CreateAttachment(Attachment attachment);
        Task<bool> DeleteAttachment(int id);
    }
}
