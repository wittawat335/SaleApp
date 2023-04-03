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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RoleMenu> _roleMenuRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userRepository, IGenericRepository<RoleMenu> roleMenuRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleMenuRepository = roleMenuRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> GetList(int userId)
        {
            IQueryable<User> tbUser = await _userRepository.GetList(x => x.UserId == userId);
            IQueryable<RoleMenu> tbRoleMenu = await _roleMenuRepository.GetList();
            IQueryable<Menu> tbMenu = await _menuRepository.GetList();

            try
            {
                IQueryable<Menu> tbResult = (from u in tbUser
                                             join mr in tbRoleMenu on u.IdRole equals mr.IdRole
                                             join m in tbMenu on mr.IdMenu equals m.MenuId
                                             select m).AsQueryable();

                var listMenus = tbResult.ToList();
                return _mapper.Map<List<MenuDTO>>(listMenus);
            }
            catch
            {
                throw;
            }
        }
    }
}
