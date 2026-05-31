using System;
using System.Collections.Generic;

namespace Clinic_Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }


    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
