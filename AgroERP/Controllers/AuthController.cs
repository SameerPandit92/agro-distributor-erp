using AgroERP.Application.DTOs.AssignRoleDto;
using AgroERP.Application.DTOs.Auth;
using AgroERP.Application.Interfaces;
using AgroERP.Application.Services;
using AgroERP.Authorization;
using AgroERP.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;



namespace AgroERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service =service;
        }
        [EnableRateLimiting("fixed")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);
            return Ok(new ApiResponse<AuthResponseDto>
            {
               Success = true,
               Message = "Login Success",
               Data = result
            });
        }

        [HttpPost("user registration")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            //return Ok( new{ message = result });
            return Ok(new ApiResponse<AuthResponseDto> { Success = true, Message = "User Register Successfully"});

        }

        [HttpGet("hash")]
        public IActionResult HashPassword(string password)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(password);
            return Ok(hashed);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole( AssignRoleDto dto)
        {
            var result = await _service.AssignRoleAsync(dto);
            return Ok(result);
        }

        [PermissionAuthorize("Sale","Create")]
        [HttpPost]
        public IActionResult AddSale()
        {
            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var result = await _service.RefreshTokenAsync(dto);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto dto)
        {
            var response = await _service.ProcessForgetPasswordAsync(dto);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var response = await _service.ProcessResetPasswordAsync(dto);
            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}
