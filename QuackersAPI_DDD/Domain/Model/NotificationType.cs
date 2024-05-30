using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class NotificationType
{
    public int NotificationType_Id { get; set; }

    public string? NotificationType_Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
