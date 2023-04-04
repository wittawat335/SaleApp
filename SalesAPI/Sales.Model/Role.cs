using System;
using System.Collections.Generic;

namespace Sales.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
