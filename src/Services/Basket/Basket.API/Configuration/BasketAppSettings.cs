using Microsoft.Extensions.Configuration;
using System;

namespace Basket.API.Configuration
{
  public class BasketAppSettings
  {
    private readonly IConfiguration _configuration;
    private Uri _discountUrl;

    public BasketAppSettings(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string CacheSettings { get => _configuration.GetValue<string>("CacheSettings:ConnectionString"); }
    public Uri DiscountUrl
    { 
      get
      {
        if (_discountUrl == null)
          _discountUrl = new Uri(_configuration.GetValue<string>("GrpcSettings:DiscountUrl"));

        return _discountUrl;
      }
    }
  }
}
