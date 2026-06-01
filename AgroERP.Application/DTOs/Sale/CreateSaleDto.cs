using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Sale
{
    public class CreateSaleDto
    {
        public int RetailerId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsCredit { get; set; }
    }
}
