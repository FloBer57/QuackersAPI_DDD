using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.AttachmentDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IAttachmentService
    {
        Task<IEnumerable<Attachment>> GetAllAttachments();
        Task<Attachment> GetAttachmentById(int id);
        Task<Attachment> CreateAttachment(CreateAttachmentDTO dto);
        Task<bool> DeleteAttachment(int id);
    }
}
