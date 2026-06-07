using AgroERP.Application.Interfaces;
using AgroERP.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController( IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async
        Task<IActionResult> GetDashboard()
        {
            var data = await _service.GetDashboardAsync();
            return Ok(data);
        }
    }
}
