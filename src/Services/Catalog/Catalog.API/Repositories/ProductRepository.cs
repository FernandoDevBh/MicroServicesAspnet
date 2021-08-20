﻿using MongoDB.Driver;
using Catalog.API.Data;
using Catalog.API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await _context
                      .Products
                      .Find(filter => true)
                      .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
      return await _context
                      .Products
                      .Find(p => p.Id == id)
                      .FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByName(string name)
    {
      var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
      return await _context
                      .Products
                      .Find(filter)
                      .FirstOrDefaultAsync();
    }    

    public async Task<Product> GetProductByCategory(string category)
    {
      var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
      return await _context
                      .Products
                      .Find(filter)
                      .FirstOrDefaultAsync();
    }

    public async Task CreateProduct(Product product)
    {
      await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
      var updateResult = await _context
                              .Products
                              .ReplaceOneAsync(filter: f => f.Id == product.Id, replacement: product);
      return updateResult.IsAcknowledged &&
             updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
      var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

      var deleteResult = await _context
                                  .Products
                                  .DeleteOneAsync(filter);

      return deleteResult.IsAcknowledged &&
             deleteResult.DeletedCount > 0;
    }
  }
}
