using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.Entities
{
    public class Item : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
        public ItemType Type { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}