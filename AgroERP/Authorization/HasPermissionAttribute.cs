using Microsoft.AspNetCore.Authorization;

namespace AgroERP.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public
        HasPermissionAttribute(string module,string permission)
        {
            Policy = $"{module}:{permission}";
        }
    }
}
