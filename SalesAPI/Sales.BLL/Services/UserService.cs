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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sales.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetList()
        {
            try
            {
                var query = await _repository.GetList();
                var list = query.Include(x => x.IdRoleNavigation).ToList();

                return _mapper.Map<List<UserDTO>>(list);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SessionDTO> Login(string email, string password)
        {
            try
            {
                var query = await _repository.GetList(x => x.Email == email && x.Password == password);
                if (query.FirstOrDefault() == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                User checkUser = query.Include(x => x.IdRoleNavigation).First();

                return _mapper.Map<SessionDTO>(checkUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO model)
        {
            try
            {
                var createUser = await _repository.Create(_mapper.Map<User>(model));
                if (createUser.UserId == 0)
                {
                    throw new TaskCanceledException(Constants.StatusMessage.Could_Not_Create);
                }
                var query = await _repository.GetList(x => x.UserId == createUser.UserId);
                createUser = query.Include(x => x.IdRoleNavigation).First();

                return _mapper.Map<UserDTO>(createUser);
            }
            catch { 
                throw; 
            }
        }

        public async Task<bool> Update(UserDTO model)
        {
            try
            {
                var userMap = _mapper.Map<User>(model);
                var UpdateUser = await _repository.Search(x => x.UserId == userMap.UserId);
                if (UpdateUser == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                UpdateUser.FullName = userMap.FullName;
                UpdateUser.Email = userMap.Email;
                UpdateUser.IdRole = userMap.IdRole;
                UpdateUser.Password = userMap.Password;
                UpdateUser.IsActive = userMap.IsActive;

                bool updated = await _repository.Update(UpdateUser);
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
                var query = await _repository.Search(x => x.UserId == id);
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
