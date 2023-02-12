namespace FoodOrderAPI.Models.DTOs
{
    public class OrderDTO
    {
        public string CustomerName { get; set; }
        public bool IsPaid { get; set; }
        public int TableId { get; set; }
        public List<OrderedItemDTO> Items { get; set; }
    }
}