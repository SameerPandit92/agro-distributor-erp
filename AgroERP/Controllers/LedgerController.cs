using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerService  _ledgerService;

        public LedgerController(ILedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        [HttpGet("{retailerId}")]
        public async Task<IActionResult> GetLedger(int retailerId)
        {
            var data =await _ledgerService.GetCustomerLedgerAsync(retailerId);
            return Ok(data);
        }
    }
}
