using Microsoft.Extensions.Configuration;

namespace Catalog.API.Configuration
{
  public class MongoDbConfiguration
  {
    private readonly IConfiguration _configuration;

    public MongoDbConfiguration(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string ConnectionString { get => _configuration.GetValue<string>("DataBaseSettings:ConnectionString"); }
    public string DatabaseName { get => _configuration.GetValue<string>("DataBaseSettings:DatabaseName"); }
    public string CollectionName { get => _configuration.GetValue<string>("DataBaseSettings:CollectionName"); }
  }
}
