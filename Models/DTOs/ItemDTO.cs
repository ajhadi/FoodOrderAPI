using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
        public ItemType Type { get; set; }
    }
}