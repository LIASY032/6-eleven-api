using System;
using Item.API.Configurations;
using Item.API.Data;
using Item.API.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Item.API.Test
{
	public class Startup
	{
		public Startup()
		{
		}
        public void ConfigureServices(IServiceCollection services)
        {

            //var configuration = new ConfigurationBuilder()
            //        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //        .AddJsonFile("appsettings.json", false, true)
            //        .Build();


            services.AddAutoMapper(typeof(MapperConfig));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IItemContext, ItemContext>();
        }
    }
}

