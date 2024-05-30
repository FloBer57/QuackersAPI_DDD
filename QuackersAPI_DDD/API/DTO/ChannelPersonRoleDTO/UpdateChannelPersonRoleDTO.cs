using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelPersonRoleDTO
{
    public class UpdateChannelPersonRoleDTO
    {
        [Required(ErrorMessage = "ChannelPersonRole_name is required.")]
        [StringLength(50, ErrorMessage = "ChannelPersonRole_name must be at most 50 characters long.")]
        public string ChannelPersonRole_Name { get; set; }
    }
}
