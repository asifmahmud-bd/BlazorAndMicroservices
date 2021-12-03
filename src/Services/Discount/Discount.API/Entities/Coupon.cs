namespace Discount.API.Entities
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string ProductName { get; set; }

        public decimal Amount { get; set; }
    }
}
