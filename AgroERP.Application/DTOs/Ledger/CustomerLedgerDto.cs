using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Ledger
{
    public class CustomerLedgerDto
    {
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; }  = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
