using System.ComponentModel.DataAnnotations;
using FoodOrderAPI.Models.Enums;

namespace FoodOrderAPI.Models.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public Order ActiveOrder { get; set; }
    }
}