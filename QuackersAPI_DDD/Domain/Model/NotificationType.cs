using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class NotificationType
{
    public int NotificationType_Id { get; set; }

    public string? NotificationType_Name { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
