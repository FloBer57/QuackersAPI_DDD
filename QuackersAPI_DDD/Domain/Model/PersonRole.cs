using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Personrole
{
    public int PersonRole_Id { get; set; }

    public string PersonRole_Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
