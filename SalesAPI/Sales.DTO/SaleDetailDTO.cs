using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTO
{
    public class SaleDetailDTO
    {
        //public int Id { get; set; }

        //public int? IdSales { get; set; }

        public int? IdProduct { get; set; }
        public string? ProductName { get; set; }

        public int? Quantity { get; set; }

        public string? PriceText { get; set; }

        public string? TotalText { get; set; }
    }
}
