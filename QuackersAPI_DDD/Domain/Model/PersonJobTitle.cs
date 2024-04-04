using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Personjobtitle
{
    public int PersonJob_TitleId { get; set; }

    public string? PersonJobTitle_Name { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
