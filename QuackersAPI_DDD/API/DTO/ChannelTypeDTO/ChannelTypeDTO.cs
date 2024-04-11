using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelTypeDTO
{
    public class ChannelTypeDTO
    {
        [Required(ErrorMessage = "Channel type name is required.")]
        [StringLength(50, ErrorMessage = "Channel type name must be at most 50 characters long.")]
        public string ChannelType_Name { get; set; }
    }

}
