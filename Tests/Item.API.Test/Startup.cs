using System;
using Item.API.Configurations;
using Item.API.Data;
using Item.API.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Item.API.Test
{
	public class Startup
    {
        public HttpClient _client { get; }
      
        //public void ConfigureHost(IHostBuilder hostBuilder)
        //{
        //    hostBuilder
        //        .ConfigureHostConfiguration(builder =>
        //        {
        //            builder.AddJsonFile("appsettings.json", true);
        //        })
        //        .ConfigureWebHostDefaults(builder =>
        //        {
        //            builder.UseStartup<Program>();

        //            builder.UseTestServer();
        //            builder.ConfigureServices(services =>
        //            {
        //                services.AddSingleton(sp => sp.GetRequiredService<IHost>()
        //                    .GetTestClient()
        //                );
        //            });
        //        })
        //        ;
        //}
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

