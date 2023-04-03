using AutoMapper;
using Sales.BLL.Services.Contract;
using Sales.DAL.Repository.Contract;
using Sales.DTO;
using Sales.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetList()
        {
            try
            {
                var list = await _repository.GetList();
                return _mapper.Map<List<CategoryDTO>>(list.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
