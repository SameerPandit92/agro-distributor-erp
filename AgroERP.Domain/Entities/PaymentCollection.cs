using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Domain.Entities
{
    public class PaymentCollection
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CollectionDate{ get; set; } = DateTime.UtcNow;
        public string Remarks  { get; set; } = string.Empty;
        public Retailer? Retailer { get; set; }
    }
}
