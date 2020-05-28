using Microsoft.EntityFrameworkCore;

namespace Sales.Orders
{
    public class OrdersDbContext : DbContext
    {
        private const string Schema = "sales";
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPlaced> OrdersPlaced { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=dcommerce;Username=dcommerce;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}