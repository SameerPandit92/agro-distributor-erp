using Microsoft.AspNetCore.Authorization;

namespace AgroERP.Authorization
{
        public class PermissionAuthorizeAttribute : AuthorizeAttribute
        {
            public PermissionAuthorizeAttribute(string module, string permission)
            {
                Policy = $"{module}:{permission}";
            }
        }
    
}
