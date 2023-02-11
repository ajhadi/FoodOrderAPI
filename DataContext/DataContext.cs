global using Microsoft.EntityFrameworkCore;
using FoodOrderAPI.Extensions;

namespace FoodOrderAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var db = configuration.GetValue<string>("ConnectionStrings:FoodOrderDb");
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:FoodOrderDb"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}