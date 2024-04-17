using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Channel
{
    public int Channel_Id { get; set; }

    public string Channel_Name { get; set; } = null!;

    public string? Channel_ImagePath { get; set; }

    public int ChannelType_Id { get; set; }
    [JsonIgnore]
    public virtual ChannelType ChannelType { get; set; }
    [JsonIgnore]
    public virtual ICollection<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; } = new List<ChannelPersonRoleXPersonXChannel>();
    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    [JsonIgnore]
    public virtual ICollection<PersonXChannel> Personxchannels { get; set; } = new List<PersonXChannel>();
}
