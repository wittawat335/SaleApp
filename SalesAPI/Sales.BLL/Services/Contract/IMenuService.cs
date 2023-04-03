using Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.Contract
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetList(int userId);
    }
}
