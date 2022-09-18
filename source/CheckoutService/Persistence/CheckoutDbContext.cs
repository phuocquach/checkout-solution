using Microsoft.EntityFrameworkCore;
namespace CheckoutService.Persistence
{
    public class CheckoutDBContext : DbContext
    {
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(x => x.HasMany(x => x.BasketProducts));
        }
        
    }
}
