using System;
using System.Collections.Generic;

namespace Sales.Model;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int? IdRole { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? PasswordHash { get; set; }

    public string? Token { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
