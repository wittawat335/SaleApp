using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services.Contract
{
    public interface IPasswordHasService
    {
        string Hash(string password);
        bool Verify(string passwordHash, string inputPassword);
    }
}
