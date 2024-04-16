using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonJobTitle
{
    public int PersonJob_TitleId { get; set; }

    public string? PersonJobTitle_Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
