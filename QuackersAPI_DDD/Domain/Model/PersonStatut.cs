using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class Personstatut
{
    public int PersonStatut_Id { get; set; }

    public string PersonStatut_Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
