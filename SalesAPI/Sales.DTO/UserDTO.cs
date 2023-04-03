using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? IdRole { get; set; }
        public string? RoleName { get; set; }
        public string? Password { get; set; }
        public int? IsActive { get; set; }
    }
}
