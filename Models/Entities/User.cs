using System.ComponentModel.DataAnnotations;
namespace FoodOrderAPI.Models.Entities
{
    public class User: BaseEntity<Guid>
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
    }
}