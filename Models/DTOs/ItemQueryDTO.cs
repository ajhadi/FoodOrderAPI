using System.ComponentModel.DataAnnotations;
using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class ItemQueryDTO
    {
        [EnumDataType(typeof(ItemType))]
        public ItemType? Type { get; set; }
    }
}