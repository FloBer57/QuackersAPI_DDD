using QuackersAPI_DDD.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel
{
    public class UpdateChannelPersonRoleXPersonXChannelDTO
    {
        [Required(ErrorMessage = "ChannelPersonRole_Id is required.")]
        public int ChannelPersonRoleId { get; set; }
    }
}
