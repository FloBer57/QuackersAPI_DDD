using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class ChannelType
{
    public int ChannelType_Id { get; set; }

    public string ChannelType_Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
