using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonXChannel
{
    public int Person_Id { get; set; }

    public int Channel_Id { get; set; }

    public DateTime? PersonXchannelSignInDate { get; set; }
    [JsonIgnore]
    public virtual Channel Channel { get; set; } = null!;
    [JsonIgnore]
    public virtual Person Person { get; set; } = null!;
}
