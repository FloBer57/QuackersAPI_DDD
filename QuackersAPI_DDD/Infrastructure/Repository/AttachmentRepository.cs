using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.Database;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Infrastructure.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Attachment> GetAttachmentById(int id)
        {
            return await _context.Attachments.FindAsync(id);
        }
        public async Task<IEnumerable<Attachment>> GetAttachmentsByMessageId(int messageId)
        {
            return await _context.Attachments
                                 .Where(a => a.Message_Id == messageId)
                                 .ToListAsync();
        }


        public async Task<Attachment> CreateAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }

        public async Task<bool> DeleteAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AttachmentNameExists(string attachmentName)
        {
            return await _context.Attachments.AnyAsync(a => a.Attachment_Name == attachmentName);
        }
    }
}
