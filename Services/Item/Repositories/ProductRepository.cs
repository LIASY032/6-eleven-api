using System;
using System.Xml.Linq;
using Item.API.Data;
using Item.API.Entities;
using MongoDB.Driver;

namespace Item.API.Repositories
{
	 public class ProductRepository: IProductRepository
	{
        private readonly IItemContext _context;

        public ProductRepository(IItemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return await _context.Products.Find(p => p.Title == product.Title).FirstOrDefaultAsync();
        }

        public async Task<Product> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            
          var deleteResult = await _context.Products.FindOneAndDeleteAsync(filter);

            return deleteResult;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                          .Products
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        public async Task<Product> GetProduct(Product product)
        {

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p =>  true )
                            .ToListAsync();

        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                .Products
                                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}

