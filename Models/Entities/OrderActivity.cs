using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.Entities
{
    public class OrderActivity : BaseEntity<int>
    {
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid OrderId { get; set; }
    }
}