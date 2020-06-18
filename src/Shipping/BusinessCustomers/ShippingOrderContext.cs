using Microsoft.EntityFrameworkCore;
using Shipping.Framework;

namespace Shipping.BusinessCustomers
{
    public class ShippingOrderContext : DbContext
    {
        private const string Schema = "shipping";

        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<PersistedEvent> PersistedEvents { get; set; }
        public DbSet<EventContext> EventContexts { get; set; } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=dcommerce;Username=dcommerce;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}