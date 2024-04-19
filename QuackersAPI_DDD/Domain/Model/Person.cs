using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Person
{
    public int Person_Id { get; set; }

    public string? Person_Password { get; set; }

    public string? Person_Email { get; set; }

    public string? Person_PhoneNumber { get; set; }

    public string Person_FirstName { get; set; } = null!;

    public string Person_LastName { get; set; } = null!;

    public DateTime? Person_CreatedTimePerson { get; set; } 

    public string? Person_ProfilPicturePath { get; set; } 

    public string? Person_Description { get; set; } 

    public string? Person_TokenResetPassword { get; set; }

    public bool Person_IsTemporaryPassword { get; set; } = true;

    public int PersonJobTitle_Id { get; set; }

    public int PersonStatut_Id { get; set; }

    public int PersonRole_Id { get; set; }
    [JsonIgnore]
    public virtual ICollection<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; } = new List<ChannelPersonRoleXPersonXChannel>();
    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    [JsonIgnore]
    public virtual ICollection<MessageXReactionXPerson> Messagexreactionxpeople { get; set; } = new List<MessageXReactionXPerson>();
    [JsonIgnore]
    public virtual PersonJobTitle PersonJobTitle { get; set; } = null!;
    [JsonIgnore]
    public virtual PersonRole PersonRole { get; set; } = null!;
    [JsonIgnore]
    public virtual PersonStatut PersonStatut { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<PersonXChannel> Personxchannels { get; set; } = new List<PersonXChannel>();
    [JsonIgnore]
    public virtual ICollection<PersonXMessage> Personxmessages { get; set; } = new List<PersonXMessage>();
    [JsonIgnore]
    public virtual ICollection<PersonXNotification> Personxnotifications { get; set; } = new List<PersonXNotification>();
}
