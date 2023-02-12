using FoodOrderAPI.Services.TokenService;
using static Constants;

namespace FoodOrderAPI.Helper
{
    public class DataMigrationHelper
    {
        /// <summary>Process of populating a database with an initial set of data.</summary>
        public static void SeedData(ModelBuilder modelBuilder, ITokenService tokenService)
        {
            {
                tokenService.CreatePasswordHash("admin", out byte[] passwordHash, out byte[] passwordSalt);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "admin",
                        Role = UserRole.Admin,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );
            }
            {
                tokenService.CreatePasswordHash("cashier", out byte[] passwordHash, out byte[] passwordSalt);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "cashier",
                        Role = UserRole.Cashier,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );
            }
            {
                tokenService.CreatePasswordHash("waiter1", out byte[] passwordHash, out byte[] passwordSalt);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "waiter1",
                        Role = UserRole.Waiter,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );
            }
            {
                tokenService.CreatePasswordHash("waiter2", out byte[] passwordHash, out byte[] passwordSalt);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "waiter2",
                        Role = UserRole.Waiter,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    }
                );
            }


            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Spicy Crispy Chicken Sandwich",
                    Price = 35000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Food
                },
                new Item
                {
                    Id = 2,
                    Name = "Big Mac",
                    Price = 41000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Food
                },
                new Item
                {
                    Id = 3,
                    Name = "Sausage Burrito",
                    Price = 38000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Food
                },
                new Item
                {
                    Id = 4,
                    Name = "Ordinary Fries",
                    Price = 21000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Food
                },
                new Item
                {
                    Id = 5,
                    Name = "Pizza",
                    Price = 80000,
                    IsReady = false,
                    Type = Models.Enums.ItemType.Food
                },
                new Item
                {
                    Id = 6,
                    Name = "Sprite",
                    Price = 21000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Beverage
                },
                new Item
                {
                    Id = 7,
                    Name = "Cola-Cola",
                    Price = 21000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Beverage
                },
                new Item
                {
                    Id = 8,
                    Name = "Caramel Macchiato",
                    Price = 35000,
                    IsReady = true,
                    Type = Models.Enums.ItemType.Beverage
                },
                new Item
                {
                    Id = 9,
                    Name = "Cappuccino",
                    Price = 30000,
                    IsReady = false,
                    Type = Models.Enums.ItemType.Beverage
                },
                new Item
                {
                    Id = 10,
                    Name = "Caramel Cappuccino",
                    Price = 33000,
                    IsReady = false,
                    Type = Models.Enums.ItemType.Beverage
                }
            );
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Name = "Table 1", IsReady = true },
                new Table { Id = 2, Name = "Table 2", IsReady = true },
                new Table { Id = 3, Name = "Table 3", IsReady = true },
                new Table { Id = 4, Name = "Table 4", IsReady = true },
                new Table { Id = 5, Name = "Table 5", IsReady = true },
                new Table { Id = 6, Name = "Table 6", IsReady = true },
                new Table { Id = 7, Name = "Table 7", IsReady = true },
                new Table { Id = 8, Name = "Table 8", IsReady = true },
                new Table { Id = 9, Name = "Table 9", IsReady = true },
                new Table { Id = 10, Name = "Table 10", IsReady = true }
            );
        }
    }
}