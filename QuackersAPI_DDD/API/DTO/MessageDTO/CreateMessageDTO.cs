using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.MessageDTO
{
    public class CreateMessageDTO
    {
        [Required(ErrorMessage = "Message Text is required.")]
        [StringLength(500, ErrorMessage = "Message Text must be at most 500 characters long.")]
        public string MessageText { get; set; }
        [Required(ErrorMessage = "Channel_Id is required.")]
        public int ChannelId { get; set; }
        [Required(ErrorMessage = "Person_Id name is required.")]
        public int PersonId { get; set; }
    }
}
