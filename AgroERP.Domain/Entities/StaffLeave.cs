using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Domain.Entities
{
    public class StaffLeave
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public DateTime LeaveDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public Staff? Staff { get; set; }
    }
}
