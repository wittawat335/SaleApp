using Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetList();
        Task<UserDTO> Create(UserDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(int id);
        Task<SessionDTO> Login(string email, string password);
    }
}
