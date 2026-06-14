using AgroERP.Application.DTOs.Sale;
using AgroERP.Application.Interfaces;
using AgroERP.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController( ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HasPermission("Sale", "Create")]
        [HttpPost]
        public async Task<IActionResult> AddSale( CreateSaleDto dto)
        {
            await _saleService.AddAsync(dto);
            return Ok("Sale Added Successfully");
        }
    }
}
