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
    public class CatalogRepository : BaseRepository<Product>, ICatalogRepository
    {
        private readonly ApiDbContext _dbContext;
        
        public CatalogRepository(ApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
