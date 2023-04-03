using Microsoft.EntityFrameworkCore;
using Sales.DAL.DBContext;
using Sales.DAL.Repository.Contract;
using Sales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DAL.Repository
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbsalesContext _dbContext;

        public GenericRepository(DbsalesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TModel> Search(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch { throw; }
        }

        public async Task<IQueryable<TModel>> GetList(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {
               IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
               return queryModel;
            }
            catch { throw; }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch { throw; }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }
      
    }
}
