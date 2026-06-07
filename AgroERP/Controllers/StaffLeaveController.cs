using AgroERP.Application.DTOs.Leave;
using AgroERP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffLeaveController : ControllerBase
    {
        private readonly IStaffLeaveService  _service;

        public StaffLeaveController(IStaffLeaveService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddLeave(CreateStaffLeaveDto dto)
        {
            await _service.AddAsync(dto);

            return Ok("Leave Applied Successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data =await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(int leaveId,string status)
        {
            await _service.UpdateStatusAsync(leaveId,status);
            return Ok("Leave Status Updated");
        }
    }
}
