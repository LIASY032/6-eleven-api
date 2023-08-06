using System;
using System.Xml.Linq;
using Item.API.Data;
using Item.API.Entities;
using MongoDB.Bson;
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
            return await _context.Products.Find(p => p.Title.Contains(product.Title) || p.Info.Contains(product.Info) || p.Price == product.Price || p.WeeklyDeal == product.WeeklyDeal || p.CollectionType == product.CollectionType).FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<Product>> GetProducts(Product product)
        {
            return await _context.Products.Find(p => p.Title.Contains(product.Title) || p.Info.Contains(product.Info) || p.Price == product.Price || p.WeeklyDeal == product.WeeklyDeal || p.CollectionType == product.CollectionType).ToListAsync();

        }
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _context.Products.Find(p => p.CollectionType.Contains(categoryName)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByTitle(string title)
        {
            return await _context.Products.Find(p => p.Title.Contains(title)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p =>  true )
                            .ToListAsync();

        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                .Products
                                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            if (updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0) {
                return product;
            }
            else {
                return null;
	    }
        }
    }
}

