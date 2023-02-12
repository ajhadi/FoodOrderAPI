global using Microsoft.EntityFrameworkCore;
using FoodOrderAPI.Extensions;
using FoodOrderAPI.Services.TokenService;

namespace FoodOrderAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;

        public DataContext(DbContextOptions<DataContext> options,
        IConfiguration configuration,
        ITokenService tokenService) : base(options)
        {
            this.configuration = configuration;
            this.tokenService = tokenService;
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

            modelBuilder.Entity<User>().HasIndex(o => o.Username).IsUnique();
            modelBuilder.Entity<Item>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderId, o.ItemId });
            modelBuilder.Entity<Table>().HasQueryFilter(e => !e.IsDeleted);

            // Data seeding
            Helper.DataMigrationHelper.SeedData(modelBuilder, tokenService);

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderActivity> OrderActivities { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}