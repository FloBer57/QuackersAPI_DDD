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
            return await _attachmentRepository.GetAttachmentById(id);
        }


        public async Task<Attachment> CreateAttachment(CreateAttachmentDTO dto)
        {
            if (await _attachmentRepository.AttachmentNameExists(dto.Attachment_Name))
            {
                throw new InvalidOperationException("An attachment with the same name already exists.");
            }

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
            var attachment = _attachmentRepository.DeleteAttachment(id);
            if (attachment != null)
            {
                throw new InvalidOperationException("Attachment with id {id} not found");
            }
            return true;
        }
    }
}
