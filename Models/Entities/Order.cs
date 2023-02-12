using System.ComponentModel.DataAnnotations;
using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.Entities
{
    public class Order : BaseEntity<Guid>
    {
        [Required]
        public string OrderNumber { get; set; } = Helper.StringGenerator.GenerateOrderNumber();
        public string CustomerName { get; set; }
        public int Total { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus Status { get; set; }
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderActivity> Activities { get; set; }
    }
}