using AgroERP.Application.DTOs.Attendance;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAttendanceController : ControllerBase
    {
        private readonly IStaffAttendanceService _service;

        public StaffAttendanceController(IStaffAttendanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async  Task<IActionResult> AddAttendance(CreateAttendanceDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("Attendance Added Successfully");
        }
    }
}
