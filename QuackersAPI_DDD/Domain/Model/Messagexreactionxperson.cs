using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class MessageXReactionXPerson
{
    public int Person_Id { get; set; }

    public int Message_Id { get; set; }

    public int Reaction_Id { get; set; }

    public DateTime? MessageXreactionXpersonReactionDate { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;
}
