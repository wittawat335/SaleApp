using AutoMapper;
using Sales.BLL.Services.Contract;
using Sales.DAL.Repository.Contract;
using Sales.DTO;
using Sales.Model;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DashBoardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        private IQueryable<Sale> sales(IQueryable<Sale> tbSale, int quantityDay)
        {
            DateTime? date = tbSale.OrderByDescending(x => x.RecordDate).Select(y => y.RecordDate).First();

            date = date.Value.AddDays(quantityDay);

            return tbSale.Where(x => x.RecordDate.Value.Date >= date.Value.Date);
        }
        private async Task<int> totalSalesLastWeek()
        {
            int total = 0;
            IQueryable<Sale> query = await _saleRepository.GetList();

            if (query.Count() > 0)
            {
                var tbSale = sales(query, -7);
                total = tbSale.Count();
            }

            return total;
        }
        private async Task<string> totalIncomeLastWeek()
        {
            decimal result = 0;
            IQueryable<Sale> query = await _saleRepository.GetList();

            if (query.Count() > 0)
            {
                var tbSale = sales(query, -7);
                result = tbSale.Select(x => x.Total).Sum(y => y.Value);
            }

            return Convert.ToString(result, new CultureInfo(Constants.CultureInfoFormat.en_US));
        }
        private async Task<int> totalProduct()
        {
            IQueryable<Product> query = await _productRepository.GetList();
            int total = query.Count();

            return total;
        }
        private async Task<Dictionary<string, int>> salesLastWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            IQueryable<Sale> query = await _saleRepository.GetList();

            if (query.Count() > 0)
            {
                var tbSale = sales(query, -7);

                result = tbSale
                    .GroupBy(x => x.RecordDate.Value.Date).OrderBy(y => y.Key)
                    .Select(y => new { date = y.Key.ToString(Constants.DateTimeFormat.ddMMyyyy), total = y.Count() })
                    .ToDictionary(keySelector: z => z.date, elementSelector: z => z.total);
            }
            return result;
        }
        public async Task<DashBoardDTO> Summary()
        {
            DashBoardDTO dashBoard = new DashBoardDTO();
            try
            {
                dashBoard.SalesTotal = await totalSalesLastWeek();
                dashBoard.TotalIncome = await totalIncomeLastWeek();
                dashBoard.TotalProduct = await totalProduct();

                List<SalesWeekDTO> list = new List<SalesWeekDTO>();
                foreach (KeyValuePair<string, int> item in await salesLastWeek())
                {
                    list.Add(new SalesWeekDTO()
                    {
                        Date = item.Key,
                        Total = item.Value,
                    });
                }

                dashBoard.listSalesWeek = list;
            }
            catch
            {
                throw;
            }

            return dashBoard;
        }
    }
}
