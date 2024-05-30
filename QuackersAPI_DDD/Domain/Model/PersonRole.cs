using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonRole
{
    public int PersonRole_Id { get; set; }

    public string PersonRole_Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
