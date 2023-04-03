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
    public class MenuController : ControllerBase
    {
       private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetList(int userId)
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.value = await _menuService.GetList(userId);
                response.status = Constants.Status.True;
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
