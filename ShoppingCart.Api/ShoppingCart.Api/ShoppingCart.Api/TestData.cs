using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Models.Data;

namespace ShoppingCart.Api
{
    public static class TestData
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApiDbContext>();
                await Seed(dbContext);
            }
        }

        public static async Task Seed(ApiDbContext dbContext)
        {
            await SeedCatalogData(dbContext);
        }

        private static async Task SeedCatalogData(ApiDbContext dbContext)
        {
            if (!dbContext.CatalogItems.Any())
            {
                dbContext.Add(new Product
                {
                    Id = Guid.Parse("13a9cd0b-dbfc-47d0-ab25-43e6d2aac375"),
                    Name = "Apple",
                    NamePlural = "Apples",
                    UnitPrice = new decimal(0.34)
                });

                dbContext.Add(new Product
                {
                    Id = Guid.Parse("117d7811-2d5d-4192-8cd3-bbd5d353981f"),
                    Name = "Banana",
                    NamePlural = "Bananas",
                    UnitPrice = new decimal(0.34)
                });
                dbContext.Add(new Product
                {
                    Id = Guid.Parse("2ca94fcf-22e3-49bb-9c7c-71a0130a9290"),
                    Name = "Orange",
                    NamePlural = "Oranges",
                    UnitPrice = new decimal(0.34)
                });
                dbContext.Add(new Product
                {
                    Id = Guid.Parse("76dec6a2-73d6-42ec-918c-d996eb02cba2"),
                    Name = "Cherry",
                    NamePlural = "Cherries",
                    UnitPrice = new decimal(0.34)
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
