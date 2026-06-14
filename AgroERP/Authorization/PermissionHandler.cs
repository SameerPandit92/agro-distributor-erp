using AgroERP.Application.Interfaces;
using AgroERP.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AgroERP.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IAuthRepository _repository;

        public PermissionHandler(IAuthRepository repository)
        {
            _repository = repository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userName =context.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return;
            }

            var user = await _repository.GetByUsernameAsync(userName);

            if (user == null)
            {
                return;
            }

            bool hasPermission =await _repository.HasPermissionAsync(user.Id, requirement.Module,requirement.Permission);

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}
    //public class PermissionHandler: AuthorizationHandler<PermissionRequirement>
    //{
    //    private readonly ApplicationDbContext _context;

    //    public PermissionHandler(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,PermissionRequirement requirement)
    //    {
    //        var userName = context.User.FindFirst(ClaimTypes.Name)?.Value;
    //        if
    //        (
    //            string.IsNullOrEmpty(userName)
    //        )
    //        {
    //            return;
    //        }
    //        var user =  await _context.AppUsers.FirstOrDefaultAsync(x =>x.UserName == userName);
    //        if (user == null)
    //        {
    //            return;
    //        }
    //        var hasPermission = await
    //            (
    //                from ur in _context.UserRoles
    //                join rp in _context.RolePermissions
    //                on ur.RoleId equals rp.RoleId 
    //                join m in _context.Modules
    //                on rp.ModuleId equals m.Id
    //                join p in _context.Permissions
    //                on rp.PermissionId equals p.Id
    //                where ur.UserId == user.Id
    //                && m.Name == requirement.Module
    //                && p.Name == requirement.Permission
    //                select rp
    //            ).AnyAsync();

    //        if (hasPermission)
    //        {
    //            context.Succeed(requirement);
    //        }
    //    }
    //}
//}