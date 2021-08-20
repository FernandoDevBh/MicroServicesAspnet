using Catalog.API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Catalog.API.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<Product> GetProductByName(string name);
    Task<Product> GetProductByCategory(string category);
    Task CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
  }
}
