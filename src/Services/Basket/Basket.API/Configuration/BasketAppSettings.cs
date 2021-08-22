using Microsoft.Extensions.Configuration;

namespace Basket.API.Configuration
{
  public class BasketAppSettings
  {
    private readonly IConfiguration _configuration;

    public BasketAppSettings(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string CacheSettings { get => _configuration.GetValue<string>("CacheSettings:ConnectionString"); }
  }
}
