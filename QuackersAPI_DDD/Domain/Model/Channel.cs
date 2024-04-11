using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Channel
{
    public int Channel_Id { get; set; }

    public string Channel_Name { get; set; } = null!;

    public string? Channel_ImagePath { get; set; } = "Path/To/Default/Image";

    public int ChannelType_Id { get; set; }

    public virtual ChannelType ChannelType { get; set; } 

    public virtual ICollection<Channelpersonrolexpersonxchannel> Channelpersonrolexpersonxchannels { get; set; } = new List<Channelpersonrolexpersonxchannel>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Personxchannel> Personxchannels { get; set; } = new List<Personxchannel>();
}
