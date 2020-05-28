using Microsoft.EntityFrameworkCore;

namespace Billing.Payments
{
    public class PaymentDetailsContext : DbContext
    {
        private const string Schema = "billing";

        public DbSet<PaymentDetails> PaymentDetailses { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Database=dcommerce;Username=dcommerce;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}