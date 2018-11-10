using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Extensions;
using ShoppingCart.Api.Infrastructure.Filters;
using ShoppingCart.Api.Repositories.Implementation;
using ShoppingCart.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace ShoppingCart.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // For testing purposes use an in memory database
            // Swap out for another such as SqlServer
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("Test");
            });

            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            // Ensure lower case URLs for routing
            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddAutoMapper();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(new SelfReferenceFilter());
                opt.AllowEmptyInputInBodyModelBinding = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ShoppingCart.Api", Version = "v1" });
                c.CustomSchemaIds((type) => type.SwaggerName());

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                TestData.Seed(app).Wait();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart.Api");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
