using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Interfaces;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Repositories.Implementation
{
    public abstract class BaseRepository<T> where T : Entity
    {
        private readonly ApiDbContext _dbContext;

        protected BaseRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> FindByIdAsync(Guid id) => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public virtual async Task<List<T>> FetchAllAsync() => await _dbContext.Set<T>().ToListAsync();
    }
}
