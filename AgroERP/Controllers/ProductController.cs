using AgroERP.Application.DTOs.Product;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly
       IProductService  _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async
            Task<IActionResult>  AddProduct(CreateProductDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Product Added Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
    }
}
