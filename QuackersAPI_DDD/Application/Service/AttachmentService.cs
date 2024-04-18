using QuackersAPI_DDD.API.DTO.AttachmentDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository;

namespace QuackersAPI_DDD.Application.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMessageRepository _messageRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository , IMessageRepository messageRepository)
        {
            _attachmentRepository = attachmentRepository;
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<Attachment>> GetAllAttachments()
        {
            var attachments =  await _attachmentRepository.GetAllAttachments();
            return attachments ?? new List<Attachment>();
        }

        public async Task<Attachment> GetAttachmentById(int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentById(id);
            if (attachment == null)
            {
                throw new KeyNotFoundException("The specified attachment ID does not exist.");
            }
            return attachment;
        }

        public async Task<List<Attachment>> CreateAttachments(CreateAttachmentDTO dto, IEnumerable<IFormFile> files)
        {
            var message = await _messageRepository.GetMessageById(dto.Message_Id);
            if (message == null)
            {
                throw new KeyNotFoundException("The specified message ID does not exist.");
            }

            List<Attachment> attachments = new List<Attachment>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string uniqueName = NameGenerator.GenerateUniqueAttachmentName(file.FileName);
                    var filePath = Path.Combine("C:\\Users\\Florent\\Documents\\GitHub\\QuackersAPI_DDD\\QuackersAPI_DDD\\DownloadTemporary\\", uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var attachment = new Attachment
                    {
                        Attachment_Name = uniqueName,
                        AttachmentThing = filePath, 
                        Message_Id = dto.Message_Id
                    };

                    attachments.Add(await _attachmentRepository.CreateAttachment(attachment));
                }
            }

            return attachments;
        }


        public async Task<bool> DeleteAttachment(int id)
        {
            var exists = await _attachmentRepository.GetAttachmentById(id);
            if (exists == null)
            {
                throw new KeyNotFoundException($"The specified attachment with id {id} does not exist.");
            }
            await _attachmentRepository.DeleteAttachment(id);
            return true;
        }
    }
}
