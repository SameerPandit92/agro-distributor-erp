using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalRetailers{ get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TodayCollection { get; set; }
        public decimal TodaySale { get; set; }
        public int TotalStaff { get; set; }
    }
}
