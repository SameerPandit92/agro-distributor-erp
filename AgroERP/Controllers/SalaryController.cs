using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _service;

        public SalaryController(ISalaryService service)
        {
            _service =service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalary(int staffId,int month,int year)
        {
            var data =await _service.CalculateSalaryAsync(staffId,month,year);
            return Ok(data);
        }
    }
}
