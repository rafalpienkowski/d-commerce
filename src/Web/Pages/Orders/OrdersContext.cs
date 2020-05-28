using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Orders
{
    public class OrdersContext : DbContext
    {
        private const string Schema = "web";
        
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=dcommerce;Username=dcommerce;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}