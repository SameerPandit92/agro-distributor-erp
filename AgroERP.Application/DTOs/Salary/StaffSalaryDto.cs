using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Salary
{
    public class StaffSalaryDto
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public decimal MonthlySalary { get; set; }
        public int PresentDays { get; set; }
        public int HalfDays { get; set; }
        public int LeaveDays { get; set; }
        public decimal FinalSalary { get; set; }
    }
}
