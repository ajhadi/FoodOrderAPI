using System.ComponentModel.DataAnnotations;
using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class OrderQueryDTO
    {
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus? Status { get; set; }
    }
}