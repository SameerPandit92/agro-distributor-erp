namespace AgroERP.Domain.Entities
{
    public class Retailer
    {
        public int Id { get; set; }

        public string ShopName { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal CreditLimit { get; set; }

        public decimal DueAmount { get; set; }

        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
    }
}
