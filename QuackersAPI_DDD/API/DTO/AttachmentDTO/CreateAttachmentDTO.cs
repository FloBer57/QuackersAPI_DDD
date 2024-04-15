using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.AttachmentDTO
{
    public class CreateAttachmentDTO
    {
        [Required(ErrorMessage = "Attachment_Name is required.")]
        [StringLength(50, ErrorMessage = "Attachment_Name name must be at most 25 characters long.")]
        public string Attachment_Name { get; set; }
        public string? AttachmentThing { get; set; }
        [Required(ErrorMessage = "Message_ID is required.")]
        public int Message_Id { get; set; }
    }
}
