using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Models.Data;

namespace ShoppingCart.Api.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> FindByIdAsync(Guid id);
        Task<List<T>> FetchAllAsync();
    }
}
