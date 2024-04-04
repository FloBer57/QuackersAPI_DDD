using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Notification
{
    public int Notification_Id { get; set; }

    public string? Notification_Name { get; set; }

    public string? Notification_Text { get; set; }

    public DateOnly Notification_DatePost { get; set; }

    public int Notification_TypeId { get; set; }

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual ICollection<Personxnotification> Personxnotifications { get; set; } = new List<Personxnotification>();
}
