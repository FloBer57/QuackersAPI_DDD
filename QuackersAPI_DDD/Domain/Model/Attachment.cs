using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Attachment
{
    public int Attachment_Id { get; set; }

    public string Attachment_Name { get; set; } = null!;

    public string? AttachmentThing { get; set; }

    public int Message_Id { get; set; }

    public virtual Message Message { get; set; } = null!;
}
