using AgroERP.Application.DTOs.Sale;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController( ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSale( CreateSaleDto dto)
        {
            await _saleService.AddAsync(dto);
            return Ok("Sale Added Successfully");
        }
    }
}
