using System;
using Item.API.Entities;

namespace Item.API.Repositories
{
	public interface IProductRepository
	{
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task<Product> GetProduct(Product product);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<Product> DeleteProduct(string id);
    }
}

