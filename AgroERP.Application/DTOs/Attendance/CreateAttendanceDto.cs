using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Attendance
{
    public class CreateAttendanceDto
    {
        public int StaffId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
