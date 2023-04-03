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
    public class ProductController : ControllerBase
    {
       private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                response.value = await _productService.GetList();
                response.status = Constants.Status.True;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO model)
        {
            var response = new Response<ProductDTO>();
            try
            {
                response.value = await _productService.Create(model);
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
        public async Task<IActionResult> UpdateUser([FromBody] ProductDTO model)
        {
            var response = new Response<bool>();
            try
            {
                response.value = await _productService.Update(model);
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
                response.value = await _productService.Delete(id);
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
