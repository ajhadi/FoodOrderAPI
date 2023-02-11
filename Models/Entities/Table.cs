using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.Entities
{
    public class Table : BaseEntity<int>
    {
        public Table()
        {
            this.OrderHistory = new HashSet<Order>();
        }
        public string Number { get; set; }
        public bool IsReady { get; set; }
        public virtual ICollection<Order> OrderHistory { get; set; }
    }
}