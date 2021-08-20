using MongoDB.Driver;
using Catalog.API.Entities;
using Catalog.API.Configuration;

namespace Catalog.API.Data
{
  public class CatalogContext : ICatalogContext
  {
    public CatalogContext(MongoDbConfiguration configuration)
    {
      var client = new MongoClient(configuration.ConnectionString);
      var database = client.GetDatabase(configuration.DatabaseName);
      Products = database.GetCollection<Product>(configuration.CollectionName);
      CatalogContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
  }
}
