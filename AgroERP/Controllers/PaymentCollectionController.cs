using AgroERP.Application.DTOs.PaymentCollection;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCollectionController : ControllerBase
    {
        private readonly IPaymentCollectionService _service;

        public
        PaymentCollectionController(IPaymentCollectionService service)
        {
            _service =service;
        }

        [HttpPost]
        public async
        Task<IActionResult> AddPayment( CreatePaymentCollectionDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Payment Collected Successfully");
        }
    }
}
