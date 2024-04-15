using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel
{
    public class ChannelPersonRoleXPersonXChannelDTO
    {
        [Required(ErrorMessage = "Person_Id is required.")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Channel_Id is required.")]
        public int ChannelId { get; set; }
        [Required(ErrorMessage = "ChannelPersonRole_Id is required.")]
        public int ChannelPersonRole_Id { get; set; }
    }
}
