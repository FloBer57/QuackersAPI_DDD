using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;
public partial class Message
{
    public int Message_Id { get; set; }

    public string? Message_Text { get; set; }

    public DateTime? Message_Date { get; set; }

    public bool Message_IsNotArchived { get; set; }

    public int Channel_Id { get; set; }

    public int Person_Id { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual Channel Channel { get; set; } = null!;

    public virtual ICollection<Messagexreactionxperson> Messagexreactionxpeople { get; set; } = new List<Messagexreactionxperson>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<Personxmessage> Personxmessages { get; set; } = new List<Personxmessage>();
}
