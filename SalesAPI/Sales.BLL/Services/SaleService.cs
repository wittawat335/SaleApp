using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.BLL.Services.Contract;
using Sales.DAL.Repository.Contract;
using Sales.DTO;
using Sales.Model;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDTO>> Record(string search, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.GetList();
            var list = new List<Sale>();
            try
            {
                if (search == "Date")
                {
                    DateTime start_date = DateTime.ParseExact(startDate, Constants.DateTimeFormat.ddMMyyyy, new CultureInfo(Constants.CultureInfoFormat.en_US));
                    DateTime end_date = DateTime.ParseExact(endDate, Constants.DateTimeFormat.ddMMyyyy, new CultureInfo(Constants.CultureInfoFormat.en_US));

                    list = await query.Where(x =>
                    x.RecordDate.Value.Date >= start_date.Date &&
                    x.RecordDate.Value.Date <= end_date.Date
                    ).Include(y => y.SaleDetails)
                    .ThenInclude(z => z.IdProductNavigation).ToListAsync();
                }
                else
                {
                    list = await query.Where(x => x.DocumentNumber == saleNumber).Include(y => y.SaleDetails)
                    .ThenInclude(z => z.IdProductNavigation).ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<SaleDTO>>(list);
        }

        public async Task<SaleDTO> Register(SaleDTO model)
        {
            try
            {
                var sale = await _saleRepository.Register(_mapper.Map<Sale>(model));
                if (sale == null)
                    throw new TaskCanceledException(Constants.StatusMessage.Could_Not_Create);

                return _mapper.Map<SaleDTO>(sale);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> Report(string startDate, string endDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.GetList();
            var list = new List<SaleDetail>();
            try
            {
                DateTime start_date = DateTime.ParseExact(startDate, Constants.DateTimeFormat.ddMMyyyy, new CultureInfo(Constants.CultureInfoFormat.en_US));
                DateTime end_date = DateTime.ParseExact(endDate, Constants.DateTimeFormat.ddMMyyyy, new CultureInfo(Constants.CultureInfoFormat.en_US));

                list = await query
                    .Include(x => x.IdProductNavigation)
                    .Include(y => y.IdSalesNavigation)
                    .Where(z => 
                        z.IdSalesNavigation.RecordDate.Value.Date >= start_date.Date &&
                        z.IdSalesNavigation.RecordDate.Value.Date <= end_date.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReportDTO>>(list);
        }
    }
}
