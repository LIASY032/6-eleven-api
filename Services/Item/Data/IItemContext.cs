using System;
using Item.API.Entities;
using MongoDB.Driver;

namespace Item.API.Data
{
	public interface IItemContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

