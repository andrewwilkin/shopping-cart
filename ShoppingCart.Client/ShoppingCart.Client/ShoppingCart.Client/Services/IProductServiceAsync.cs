using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Services
{
    public interface IProductServiceAsync
    {
        Task<(bool status,ProductList productList)> GetProductListAsync();
        Task<(bool status, Product product)> GetProductByIdAsync(Guid productId);
    }
}
