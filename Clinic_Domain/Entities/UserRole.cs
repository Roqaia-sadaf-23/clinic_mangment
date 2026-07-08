using Clinic_Domain.Entities;
using System;
using System.Collections.Generic;

namespace Clinic_Domain.Entities;

public  class UserRole
{
    public int Id { get; private set; }

    public int UserId { get;private set; }

    public int RoleId { get;private set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    private UserRole() { }

    private UserRole(User user, int roleId)
    {
        User = user;
       // UserId = user.Id;
        RoleId = roleId;
    }

    public static UserRole Create(User user, int roleId)
    {
        return new UserRole(user, roleId);
    }




}
