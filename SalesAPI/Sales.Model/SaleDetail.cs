using System;
using System.Collections.Generic;

namespace Sales.Model;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int? IdSales { get; set; }

    public int? IdProduct { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Sale? IdSalesNavigation { get; set; }
}
