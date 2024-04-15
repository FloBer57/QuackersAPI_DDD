﻿using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Channel
{
    public int Channel_Id { get; set; }

    public string Channel_Name { get; set; } = null!;

    public string? Channel_ImagePath { get; set; }

    public int ChannelType_Id { get; set; }

    public virtual ChannelType ChannelType { get; set; }

    public virtual ICollection<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; } = new List<ChannelPersonRoleXPersonXChannel>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<PersonXChannel> Personxchannels { get; set; } = new List<PersonXChannel>();
}
