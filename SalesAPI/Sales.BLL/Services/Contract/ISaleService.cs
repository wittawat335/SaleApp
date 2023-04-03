using Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sales.BLL.Services.Contract
{
    public interface ISaleService
    {
        Task<SaleDTO> Register(SaleDTO model);
        Task<List<SaleDTO>> Record(string search,string saleNumber, string startDate, string endDate);
        Task<List<ReportDTO>> Report(string startDate, string endDate);
    }
}
