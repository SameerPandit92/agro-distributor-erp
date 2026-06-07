using AgroERP.Application.DTOs.Staff;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(CreateStaffDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Staff Added Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data =await _service.GetAllAsync();
            return Ok(data);
        }
    }
}
