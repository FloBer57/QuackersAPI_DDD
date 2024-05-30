using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonXNotification
{
    public int Person_Id { get; set; }

    public int Notification_Id { get; set; }

    public DateTime? PersonXnotification_ReadDate { get; set; }
    [JsonIgnore]
    public virtual Notification Notification { get; set; } = null!;
    [JsonIgnore]
    public virtual Person Person { get; set; } = null!;
}
