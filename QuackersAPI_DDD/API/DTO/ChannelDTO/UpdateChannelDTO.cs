using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelDTO
{
    public class UpdateChannelDTO
    {
        [StringLength(50, ErrorMessage = "Channel name must be at most 50 characters long.")]
        public string? Channel_Name { get; set; }

        [StringLength(255, ErrorMessage = "Image path must be at most 255 characters long.")]
        public string? Channel_ImagePath { get; set; }
    }

}
