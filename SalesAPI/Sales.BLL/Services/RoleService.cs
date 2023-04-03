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
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _repository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _repository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetList()
        {
            try
            {
                var list = await _repository.GetList();
                return _mapper.Map<List<RoleDTO>>(list.ToList());
            }
            catch { 
                throw; 
            }
        }
    }
}
