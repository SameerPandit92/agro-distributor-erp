using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.AssignRoleDto
{
    public class AssignRoleDto
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
    }
}
