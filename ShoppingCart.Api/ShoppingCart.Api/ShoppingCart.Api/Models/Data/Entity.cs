using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Models.Interfaces;

namespace ShoppingCart.Api.Models.Data
{
    public abstract class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
