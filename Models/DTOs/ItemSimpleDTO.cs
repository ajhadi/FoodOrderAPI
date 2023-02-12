using System.ComponentModel.DataAnnotations;
using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class ItemSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
        [EnumDataType(typeof(ItemType))]
        public ItemType Type { get; set; }
    }
}