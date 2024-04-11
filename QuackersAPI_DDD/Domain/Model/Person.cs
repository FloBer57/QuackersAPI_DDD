using QuackersAPI_DDD.Domain.Utilitie;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Person
{
    public int Person_Id { get; set; }

    public string Person_Password { get; set; }

    public string Person_Email { get; set; }

    public string? Person_PhoneNumber { get; set; }

    public string Person_FirstName { get; set; } = null!;

    public string Person_LastName { get; set; } = null!;

    public DateTime? Person_CreatedTimePerson { get; set; } 

    public string? Person_ProfilPicturePath { get; set; } 

    public string? Person_Description { get; set; } 

    public string? Person_TokenResetPassword { get; set; }

    public bool Person_IsTemporaryPassword { get; set; } = true;

    public string? Person_LoggedInToken { get; set; }

    public DateTime? Person_LoggedInTokenExpirationDate { get; set; }

    public int PersonJobTitle_Id { get; set; }

    public int PersonStatut_Id { get; set; }

    public int PersonRole_Id { get; set; }
    [JsonIgnore]
    public virtual ICollection<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; } = new List<ChannelPersonRoleXPersonXChannel>();
    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    [JsonIgnore]
    public virtual ICollection<Messagexreactionxperson> Messagexreactionxpeople { get; set; } = new List<Messagexreactionxperson>();

    public virtual PersonJobTitle PersonJobTitle { get; set; } = null!;

    public virtual PersonRole PersonRole { get; set; } = null!;

    public virtual PersonStatut PersonStatut { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Personxchannel> Personxchannels { get; set; } = new List<Personxchannel>();
    [JsonIgnore]
    public virtual ICollection<Personxmessage> Personxmessages { get; set; } = new List<Personxmessage>();
    [JsonIgnore]
    public virtual ICollection<Personxnotification> Personxnotifications { get; set; } = new List<Personxnotification>();
}
