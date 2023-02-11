using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.Entities
{
    public class Item : BaseEntity<int>
    {
        public Item()
        {
            this.Orders = new HashSet<Order>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
        public ItemType Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}