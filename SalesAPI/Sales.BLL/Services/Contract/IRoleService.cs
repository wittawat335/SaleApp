using Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.Contract
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetList();
    }
}
