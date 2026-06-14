using AgroERP.Application.DTOs.AssignRoleDto;
using AgroERP.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> AssignRoleAsync(AssignRoleDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    }
}
