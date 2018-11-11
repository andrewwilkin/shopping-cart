using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Client.Models.Cart.Requests;
using ShoppingCart.Client.Models.Cart.Responses;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Services
{
    public interface ICartServiceAsync
    {
        Task<(bool status,Models.Cart.Responses.Cart cart)> CreateCartAsync();
        Task<(bool status, Models.Cart.Responses.Cart cart)> ResetCartAsync();
        Task<(bool status, Models.Cart.Responses.Cart cart)> ReplaceCartContentsAsync(CartUpdate cartUpdateRequest);
        Task<(bool status, Models.Cart.Responses.Cart cart)> AddProductToCartAsync(Product product, int quantity);
        Task<(bool status, Models.Cart.Responses.Cart cart)> ReduceProductFromCartAsync(Product product, int quantity);
        Task<(bool status, Models.Cart.Responses.Cart cart)> RemoveProductFromCartAsync(Product product);
        Task<bool> DestroyCartAsync();
        Task<(bool status, Cart cart)> GetCartAsync();
    }
}
