using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;
public partial class Message
{
    public int Message_Id { get; set; }

    public string? Message_Text { get; set; }

    public DateTime? Message_Date { get; set; }

    public bool Message_HasAttachment { get; set; }

    public int Channel_Id { get; set; }

    public int Person_Id { get; set; }
    [JsonIgnore]
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    [JsonIgnore]
    public virtual Channel Channel { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<MessageXReactionXPerson> Messagexreactionxpeople { get; set; } = new List<MessageXReactionXPerson>();
    [JsonIgnore]
    public virtual Person Person { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<PersonXMessage> Personxmessages { get; set; } = new List<PersonXMessage>();
}
