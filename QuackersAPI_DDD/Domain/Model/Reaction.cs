using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Reaction
{
    public int Reaction_Id { get; set; }

    public string Reaction_Name { get; set; } = null!;

    public string? Reaction_PicturePath { get; set; }
    [JsonIgnore]
    public virtual ICollection<MessageXReactionXPerson> Messagexreactionxpeople { get; set; } = new List<MessageXReactionXPerson>();
}
