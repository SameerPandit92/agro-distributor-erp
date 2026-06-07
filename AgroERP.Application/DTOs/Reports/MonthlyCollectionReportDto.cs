using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Reports
{
    public class MonthlyCollectionReportDto
    {
        public string RetailerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CollectionDate { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
