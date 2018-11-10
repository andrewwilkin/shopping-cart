using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Interfaces
{
    public interface ICatalogItem
    {
        Guid Id { get; }
        string Name { get; }
    }
}
