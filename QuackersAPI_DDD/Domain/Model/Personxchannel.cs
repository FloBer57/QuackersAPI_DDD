using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonXChannel
{
    public int Person_Id { get; set; }

    public int Channel_Id { get; set; }

    public DateTime? PersonXchannelSignInDate { get; set; }

    public virtual Channel Channel { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
