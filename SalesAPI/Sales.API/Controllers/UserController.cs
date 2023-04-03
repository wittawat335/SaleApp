using Microsoft.AspNetCore.Mvc;
using Sales.API.Utility;
using Sales.BLL.Services;
using Sales.BLL.Services.Contract;
using Sales.DTO;
using Sales.Utility.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<UserDTO>>();
            try
            {
                response.value = await _userService.GetList();
                response.status = Constants.Status.True;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            var response = new Response<SessionDTO>();
            try
            {
                response.value = await _userService.Login(login.Email, login.Password);
                response.status = Constants.Status.True;
            }
            catch (Exception ex){
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            var response = new Response<UserDTO>();
            try
            {
                response.value = await _userService.Create(user);
                response.status = Constants.Status.True;
                response.message = Constants.StatusMessage.Create_Action;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            var response = new Response<bool>();
            try
            {
                response.value = await _userService.Update(user);
                response.status = Constants.Status.True;
                response.message = Constants.StatusMessage.Update_Action;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.value = await _userService.Delete(id);
                response.status = Constants.Status.True;
                response.message = Constants.StatusMessage.Delete_Action;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
