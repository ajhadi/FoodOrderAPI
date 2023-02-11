global using Microsoft.EntityFrameworkCore;

namespace FoodOrderAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DataContext(DbContextOptions<DataContext> options,IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var db = configuration.GetValue<string>("ConnectionStrings:FoodOrderDb");
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:FoodOrderDb"));
        }
        public DbSet<Item> Items { get; set; }
    }
}