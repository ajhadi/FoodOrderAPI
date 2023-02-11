namespace FoodOrderAPI.Models.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            this.Items = new HashSet<Item>();
        }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public int Total { get; set; }
        public bool IsPaid { get; set; }
        public Table Table { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}