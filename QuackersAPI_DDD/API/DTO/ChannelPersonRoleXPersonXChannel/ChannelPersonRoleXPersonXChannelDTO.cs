using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel
{
    public class ChannelPersonRoleXPersonXChannelDTO
    {
        public int PersonId { get; set; }
        public int ChannelId { get; set; }
        public int ChannelPersonRole_Id { get; set; }
    }
}
