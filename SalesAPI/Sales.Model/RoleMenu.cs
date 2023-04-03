using System;
using System.Collections.Generic;

namespace Sales.Model;

public partial class RoleMenu
{
    public int MenuRoleId { get; set; }

    public int? IdMenu { get; set; }

    public int? IdRole { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
