using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Staff
{
    public class CreateStaffDto
    {
        public string FullName { get; set; }= string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Role { get; set; }= string.Empty;
        public decimal Salary { get; set; }
    }
}
