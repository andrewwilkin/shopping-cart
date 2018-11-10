using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Models.Data;

namespace ShoppingCart.Api.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        // Override EF configuration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartItem>(b =>
            {
                b.HasKey(k => new {k.CartId, k.CatalogItemId});
                b.HasOne(p => p.CatalogItem);
                b.HasOne(p => p.Cart)
                    .WithMany(m => m.CartItems)
                    .HasForeignKey(k => k.CartId);
            });
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
