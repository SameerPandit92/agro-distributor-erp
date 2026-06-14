using AgroERP.Application.DTOs.PaginationFilter;
using AgroERP.Application.DTOs.Product;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgroERP.Shared.Common;


namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService  _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult>  AddProduct(CreateProductDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Product Added Successfully");
        }

        //[HttpGet]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetProducts([FromQuery] PaginationFilterDto dto)
        {
            var result = await _service.GetPagedAsync(dto);
            return Ok(result);
        }
    }
}
