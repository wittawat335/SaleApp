using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DAL.Repository.Contract
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Search(Expression<Func<TModel, bool>> filter);
        Task<IQueryable<TModel>> GetList(Expression<Func<TModel, bool>> filter = null);
        Task<TModel> Create(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);

    }
}
