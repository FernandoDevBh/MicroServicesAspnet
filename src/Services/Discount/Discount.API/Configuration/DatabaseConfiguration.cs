using Microsoft.Extensions.Configuration;

namespace Discount.API.Configuration
{
  public class DatabaseConfiguration
  {
    private readonly IConfiguration _configuration;

    public DatabaseConfiguration(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string Server { get => _configuration.GetValue<string>("DataBaseSettings:Server"); }
    public int Port { get => _configuration.GetValue<int>("DataBaseSettings:Port"); }
    public string Database { get => _configuration.GetValue<string>("DataBaseSettings:Database"); }
    public string UserId { get => _configuration.GetValue<string>("DataBaseSettings:UserId"); }
    public string Password { get => _configuration.GetValue<string>("DataBaseSettings:Password"); }

    public override string ToString()
    {
      return $"{nameof(Server)}={Server};{nameof(Port)}={Port};{nameof(Database)}={Database};User Id={UserId};{nameof(Password)}={Password};";
    }
  }
}
