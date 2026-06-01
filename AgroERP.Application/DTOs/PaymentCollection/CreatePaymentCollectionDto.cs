using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.PaymentCollection
{
    public class CreatePaymentCollectionDto
    {
        public int RetailerId { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
