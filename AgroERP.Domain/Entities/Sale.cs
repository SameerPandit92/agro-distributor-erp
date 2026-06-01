using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public int RetailerId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCredit { get; set; }

        public DateTime SaleDate{ get; set; }  = DateTime.UtcNow;

        public Retailer? Retailer { get; set; }

        public Product? Product { get; set; }
    }
}
