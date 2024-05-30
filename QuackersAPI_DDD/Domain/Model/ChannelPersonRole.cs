using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model
{
    public class ChannelPersonRole
    {
        public int ChannelPersonRole_Id { get; set; }
        public string ChannelPersonRole_Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChannelPersonRoleXPersonXChannel> ChannelPersonRolesXPersonsXChannels { get; set; }
    }
}
