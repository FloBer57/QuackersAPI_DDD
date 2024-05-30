using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuackersAPI_DDD.Domain.Model;

public partial class PersonStatut
{
    public int PersonStatut_Id { get; set; }

    public string PersonStatut_Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
