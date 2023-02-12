namespace FoodOrderAPI.Models.Entities
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public int ItemId { get; set; }
        public int PriceSnapshot { get; set; }
        public int Quantity { get; set; }
    }
}