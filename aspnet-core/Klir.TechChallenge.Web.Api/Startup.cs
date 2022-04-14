using AutoMapper;
using Klir.TechChallenge.Domain.Entities;
using Klir.TechChallenge.Domain.Interfaces;
using Klir.TechChallenge.Infra.Data.Repository;
using Klir.TechChallenge.Service;
using Klir.TechChallenge.Service.Services;
using Klir.TechChallenge.Web.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Klir.TechChallenge.Web.Api
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200");
                                  });
            });
            services.AddScoped<IBaseRepository<Product>>(options => {
                return new BaseRepository<Product>(Configuration.GetSection("ProductJsonFile").Value);
            });
            services.AddScoped<IBaseRepository<ShoppingCart>>(options => {
                return new BaseRepository<ShoppingCart>(Configuration.GetSection("ShoppingCartJsonFile").Value);
            });
            services.AddScoped<IBaseService<Product>, BaseService<Product>>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>(); 
            //(options => {
            //    var repository = options.GetService<IBaseRepository<ShoppingCart>>();
            //    var mapper = options.GetService<IMapper>();
            //    var logger = options.GetService<ILogger<ShoppingCartService>>();
            //    return new ShoppingCartService(repository, mapper, logger);
            //});
            services.AddSingleton(new MapperConfiguration(config => 
            { 
                config.CreateMap<ProductModel, Product>();
                config.CreateMap<ItemCommand, Item>();
                config.CreateMap<ShoppingCartModel, ShoppingCart>();
            }).CreateMapper());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
