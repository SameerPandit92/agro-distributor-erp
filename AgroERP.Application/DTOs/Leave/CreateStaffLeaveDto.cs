using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Leave
{
    public class CreateStaffLeaveDto
    {
        public int StaffId { get; set; }
        public DateTime LeaveDate { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
