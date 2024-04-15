using QuackersAPI_DDD.API.DTO.AttachmentDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        }

        public async Task<IEnumerable<Attachment>> GetAllAttachments()
        {
            return await _attachmentRepository.GetAllAttachments();
        }

        public async Task<Attachment> GetAttachmentById(int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentById(id);
            if (attachment == null)
                throw new KeyNotFoundException("Attachment not found.");
            return attachment;
        }

        public async Task<Attachment> CreateAttachment(CreateAttachmentDTO dto)
        {
            var attachment = new Attachment
            {
                Attachment_Name = dto.Attachment_Name,
                AttachmentThing = dto.AttachmentThing,
                Message_Id = dto.Message_Id
            };
            return await _attachmentRepository.CreateAttachment(attachment);
        }

        public async Task<bool> DeleteAttachment(int id)
        {
            return await _attachmentRepository.DeleteAttachment(id);
        }
    }
}
