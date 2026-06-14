using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service =  service;
        }

        [HttpGet("monthly-collection")]
        public async Task<IActionResult> GetMonthlyCollection(int month,int year)
        {
            var data = await _service.GetMonthlyCollectionAsync(month,year);
            return Ok(data);
        }
    }
}
