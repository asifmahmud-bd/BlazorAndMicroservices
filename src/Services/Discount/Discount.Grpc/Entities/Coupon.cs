namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }
    }
}
