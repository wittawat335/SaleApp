using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.BLL.Services.Contract;
using Sales.DAL.Repository.Contract;
using Sales.DTO;
using Sales.Model;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetList()
        {
            try
            {

                var query = await _repository.GetList();
                var list = query.Include(x => x.IdCategoryNavigation).ToList();

                return _mapper.Map<List<ProductDTO>>(list);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDTO> Create(ProductDTO model)
        {
            try
            {
                var productCreate = await _repository.Create(_mapper.Map<Product>(model));
                if (productCreate.ProductId == 0)
                {
                    throw new TaskCanceledException(Constants.StatusMessage.Cannot_Map_Data);
                }

                return _mapper.Map<ProductDTO>(productCreate);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO model)
        {
            try
            {
                var mapper = _mapper.Map<Product>(model);
                var dataUpdate = await _repository.Search(x => x.ProductId == mapper.ProductId);
                if (dataUpdate == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                dataUpdate.Name = mapper.Name;
                dataUpdate.IdCategory = mapper.IdCategory;
                dataUpdate.Stock = mapper.Stock;
                dataUpdate.Price = mapper.Price;
                dataUpdate.IsActive = mapper.IsActive;

                bool updated = await _repository.Update(dataUpdate);
                if (!updated)
                    throw new TaskCanceledException(Constants.StatusMessage.Cannot_Update_Data);

                return updated;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var query = await _repository.Search(x => x.ProductId == id);
                if (query == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                bool deleted = await _repository.Delete(query);
                if (deleted == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                return deleted;
            }
            catch
            {
                throw;
            }
        }       
    }
}
