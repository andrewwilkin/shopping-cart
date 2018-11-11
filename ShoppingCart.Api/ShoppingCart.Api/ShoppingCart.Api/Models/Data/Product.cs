using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Models.Interfaces;

namespace ShoppingCart.Api.Models.Data
{
    public sealed class Product : Entity
    {
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public decimal UnitPrice { get; set; }  
    }
}
