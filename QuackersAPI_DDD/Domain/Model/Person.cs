using QuackersAPI_DDD.Domain.Utilitie;
using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Person
{
    public int Person_Id { get; set; }

    public string Person_Password { get; set; }

    public string Person_Email { get; set; }

    public string? Person_PhoneNumber { get; set; }

    public string Person_FirstName { get; set; } = null!;

    public string Person_LastName { get; set; } = null!;

    public DateTime? Person_CreatedTimePerson { get; set; } = DateTime.Now;

    public string? Person_ProfilPicturePath { get; set; } = "Path/To/Default/Image";

    public string? Person_Description { get; set; } = "Je suis nouveau sur Quacker!";

    public string? Person_TokenResetPassword { get; set; }

    public bool Person_IsTemporaryPassword { get; set; }

    public string? Person_LoggedInToken { get; set; }

    public DateTime? Person_LoggedInTokenExpirationDate { get; set; }

    public int PersonJobTitle_Id { get; set; } = 1;

    public int PersonStatut_Id { get; set; } = 1;

    public int PersonRole_Id { get; set; } = 1;

    public virtual ICollection<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; } = new List<ChannelPersonRoleXPersonXChannel>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Messagexreactionxperson> Messagexreactionxpeople { get; set; } = new List<Messagexreactionxperson>();

    public virtual PersonJobTitle PersonJobTitle { get; set; } = null!;

    public virtual PersonRole PersonRole { get; set; } = null!;

    public virtual PersonStatut PersonStatut { get; set; } = null!;

    public virtual ICollection<Personxchannel> Personxchannels { get; set; } = new List<Personxchannel>();

    public virtual ICollection<Personxmessage> Personxmessages { get; set; } = new List<Personxmessage>();

    public virtual ICollection<Personxnotification> Personxnotifications { get; set; } = new List<Personxnotification>();
}
