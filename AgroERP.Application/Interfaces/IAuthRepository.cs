using AgroERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser?> GetByUsernameAsync( string username);
        Task AddUserAsync(AppUser user);
        Task AssignRoleAsync( Guid userId, int roleId);
        Task<string?> GetUserRoleAsync(Guid userId);
        Task UpdateUserAsync( AppUser user);
        Task<AppUser?> GetByRefreshTokenAsync(string refreshToken);
        Task<bool> HasPermissionAsync( Guid userId,string module, string permission);

        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser?> GetByResetTokenAsync(string token);
        Task UpdateAsync(AppUser user);
    }
}
