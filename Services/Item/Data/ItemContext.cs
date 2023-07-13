using System;
using Item.API.Entities;
using MongoDB.Driver;

namespace Item.API.Data
{
	public class ItemContext: IItemContext
	{
		public ItemContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));


            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));


            //CatalogContextSeed.SeedData(Products);

        }

        public IMongoCollection<Product> Products { get; }
    }
}

