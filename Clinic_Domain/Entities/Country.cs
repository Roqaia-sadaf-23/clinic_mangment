using System;
using System.Collections.Generic;

namespace Clinic_Domain.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
