using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.DTOs.Retailer
{
    public class RetailerDto
    {
        public int Id { get; set; }
        public string ShopName { get; set; }= string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string MobileNumber { get; set; }= string.Empty;
        public string Address { get; set; }= string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal DueAmount { get; set; }
    }
}
