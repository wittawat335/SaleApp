using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Sales.BLL.Services.Contract;
using Sales.DAL.Repository.Contract;
using Sales.DTO;
using Sales.Model;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sales.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IPasswordHasService _service;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IPasswordHasService service, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
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

        public async Task<SessionDTO> Login(string email, string password, string key)
        {
            try
            {
                var user = await _repository.GetFirst(x => x.Email == email);
                var verifyPassword = _service.Verify(user.PasswordHash, password);

                if (user == null || !verifyPassword)
                    throw new TaskCanceledException("Username or password is incorrect");

                var query = await _repository.GetList(x => x.Email == email && x.Password == password);
                if (query.FirstOrDefault() == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                User checkUser = query.Include(x => x.IdRoleNavigation).First();
                if(checkUser.IsActive == true)
                    checkUser.Token = CreateToken(checkUser, key);
                else
                    throw new TaskCanceledException(Constants.StatusMessage.InActive);

                return _mapper.Map<SessionDTO>(checkUser);
            }
            catch
            {
                throw;
            }
        }

        public string CreateToken(User user, string key)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("FullName", user.FullName!),
                    new Claim("Email", user.Email!),
                };

                var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var cred = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
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
                var checkDuplicate = await _repository.GetList(x => x.Email == model.Email);
                if (checkDuplicate.FirstOrDefault() != null)
                    throw new TaskCanceledException("Email '" + model.Email + "' is already taken");

                model.PasswordHash = _service.Hash(model.Password);
                var createUser = await _repository.Create(_mapper.Map<User>(model));
                if (createUser.UserId == 0)
                    throw new TaskCanceledException(Constants.StatusMessage.Could_Not_Create);
                
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
                var UpdateUser = await _repository.GetFirst(x => x.UserId == userMap.UserId);
                if (UpdateUser == null)
                    throw new TaskCanceledException(Constants.StatusMessage.No_Data);

                UpdateUser.FullName = userMap.FullName;
                UpdateUser.Email = userMap.Email;
                UpdateUser.IdRole = userMap.IdRole;
                UpdateUser.Password = userMap.Password;
                UpdateUser.PasswordHash = _service.Hash(userMap.Password);
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
                var query = await _repository.GetFirst(x => x.UserId == id);
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
