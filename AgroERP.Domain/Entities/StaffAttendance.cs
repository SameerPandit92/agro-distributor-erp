using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Domain.Entities
{
    public class StaffAttendance
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;
        public Staff? Staff { get; set; }
    }
}
