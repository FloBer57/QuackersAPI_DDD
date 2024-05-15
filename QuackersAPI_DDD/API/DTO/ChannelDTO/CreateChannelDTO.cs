using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelDTO
{
    public class CreateChannelDTO
    {
        [Required(ErrorMessage = "Channel name is required.")]
        [StringLength(50, ErrorMessage = "Channel name must be at most 50 characters long.")]
        public string Channel_Name { get; set; }

        [Required(ErrorMessage = "Channel type is required.")]
        public int ChannelType_Id { get; set; }
    }

}
