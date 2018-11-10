using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Models.Data
{
    public sealed class Cart : Entity
    {
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        
    }
}
