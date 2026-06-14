using Microsoft.AspNetCore.Authorization;

namespace AgroERP.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Module { get; }

        public string Permission { get; }

        public PermissionRequirement( string module, string permission)
        {
            Module = module;
            Permission = permission;
        }
    }
}
