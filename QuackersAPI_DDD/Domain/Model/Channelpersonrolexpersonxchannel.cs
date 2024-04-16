using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class ChannelPersonRoleXPersonXChannel
{
    public int Person_Id { get; set; }
    public int Channel_Id { get; set; }
    public DateTime? ChannelPersonRoleXpersonXchannelAffectDate { get; set; }
    public int ChannelPersonRole_Id { get; set; }
    [JsonIgnore]
    public virtual Person Person { get; set; }
    [JsonIgnore]
    public virtual Channel Channel { get; set; }
    [JsonIgnore]
    public virtual ChannelPersonRole ChannelPersonRole { get; set; }  
}
