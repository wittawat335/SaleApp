using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTO
{
    public class ReportDTO
    {
        public string? DocumentNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public string? RecordDate { get; set; }
        public string? Product { get; set; }
        public int? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Total { get; set; }
        public string? SalesTotal { get; set; }
    }
}
