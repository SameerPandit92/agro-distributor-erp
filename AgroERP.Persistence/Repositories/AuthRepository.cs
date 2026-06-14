using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using AgroERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            try
            {
                return await _context.AppUsers.FirstOrDefaultAsync(x => x.UserName == username);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddUserAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AssignRoleAsync(Guid userId, int roleId)
        {
            var userRole = new UserRole {UserId = userId,RoleId = roleId };
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }
        public async Task<string?> GetUserRoleAsync(Guid userId)
        {
            return await
            (
                from ur in _context.UserRoles
                join r in _context.Roles
                on ur.RoleId equals r.Id
                where ur.UserId == userId
                select r.Name
            ).FirstOrDefaultAsync();
        }
        public async Task UpdateUserAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<AppUser?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x =>x.RefreshToken == refreshToken);
        }

        public async Task<bool> HasPermissionAsync(Guid userId,string module, string permission)
        {
            return await
            (
                from ur in
                _context.UserRoles

                join rp in
                _context.RolePermissions
                on ur.RoleId
                equals rp.RoleId

                join p in
                _context.Permissions
                on rp.PermissionId
                equals p.Id

                join m in
                _context.Modules
                on rp.ModuleId
                equals m.Id

                where
                ur.UserId == userId
                &&
                m.Name == module
                &&
                p.Name == permission

                select p
            )
            .AnyAsync();
        }

        

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AppUser?> GetByResetTokenAsync(string token)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x => x.PasswordResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
        }

        public async Task UpdateAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
