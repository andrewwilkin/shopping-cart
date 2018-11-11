using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Interfaces;

namespace ShoppingCart.Api.Repositories.Interfaces
{
    public interface ICatalogRepository : IBaseRepository<Product>
    {
    }
}
