using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Channeltype
{
    public int ChannelType_Id { get; set; }

    public string ChannelType_Name { get; set; } = null!;

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
