using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Notification
{
    public int Notification_Id { get; set; }

    public string? Notification_Name { get; set; }

    public string? Notification_Text { get; set; }

    public DateTime Notification_DatePost { get; set; }

    public int Notification_TypeId { get; set; }
    [JsonIgnore]
    public virtual NotificationType NotificationType { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<PersonXNotification> Personxnotifications { get; set; } = new List<PersonXNotification>();
}
