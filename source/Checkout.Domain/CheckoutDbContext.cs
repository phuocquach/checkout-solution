using Checkout.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace Checkout.Domain
{
    public class CheckoutDBContext : DbContext
    {
        public CheckoutDBContext(DbContextOptions<CheckoutDBContext> options)
          : base(options)
        {

        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(x => x.HasMany(x => x.BasketProducts));
        }
        
    }
}
