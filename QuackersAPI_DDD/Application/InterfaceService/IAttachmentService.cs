using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.AttachmentDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IAttachmentService
    {
        Task<Attachment> GetAttachmentById(int id);
        Task<List<Attachment>> CreateAttachments(CreateAttachmentDTO dto, IEnumerable<IFormFile> files);
        Task<bool> DeleteAttachment(int id);
    }
}
