using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelDTO
{
    public class ChannelDTO
    {
        [Required(ErrorMessage = "Channel name is required.")]
        [StringLength(50, ErrorMessage = "Channel name must be at most 50 characters long.")]
        public string Channel_Name { get; set; }

        [StringLength(255, ErrorMessage = "Image path must be at most 255 characters long.")]
        public string? Channel_ImagePath { get; set; } = "Path/To/Default/Image";

        [Required(ErrorMessage = "Channel type is required.")]
        public int ChannelType_Id { get; set; }
    }

}
