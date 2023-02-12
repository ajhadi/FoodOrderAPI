using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class OrderDetailDTO
    {
        public string CustomerName { get; set; }
        public bool IsPaid { get; set; }
        public int TableId { get; set; }
        public OrderStatus Status { get; set; }
    }
}