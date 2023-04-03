using Sales.DAL.DBContext;
using Sales.DAL.Repository.Contract;
using Sales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DAL.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsalesContext _dbContext;

        public SaleRepository(DbsalesContext dbContext) :base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> Register(Sale model)
        {
            Sale sale = new Sale();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in model.SaleDetails)
                    {
                        var product = _dbContext.Products.Where(x => x.ProductId == item.IdProduct).First();

                        product.Stock = product.Stock - item.Quantity;
                        _dbContext.Products.Update(product);
                    }
                    await _dbContext.SaveChangesAsync();

                    var docNumber = _dbContext.DocumentNumbers.First();

                    docNumber.LastNumber = docNumber.LastNumber + 1;
                    docNumber.RecordDate = DateTime.Now;

                    _dbContext.DocumentNumbers.Update(docNumber);
                    await _dbContext.SaveChangesAsync();

                    int digi = 4;
                    string test = string.Concat(Enumerable.Repeat("0", digi));
                    string test2 = test + docNumber.LastNumber.ToString();

                    //00001
                    test2 = test2.Substring(test2.Length - digi, digi);
                    model.DocumentNumber = test2;
                    await _dbContext.Sales.AddAsync(model);
                    await _dbContext.SaveChangesAsync();

                    sale = model;
                    transaction.Commit();

                }
                catch 
                {
                    transaction.Rollback();
                    throw;

                }

                return sale;
            }
        }
    }
}
