using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.AttachmentDTO
{
    public class CreateAttachmentDTO
    {
        [Required(ErrorMessage = "Message_ID is required.")]
        public int Message_Id { get; set; }
    }
}
