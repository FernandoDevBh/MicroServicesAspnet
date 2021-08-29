using Basket.API.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Basket.API.GrpcServices;

namespace Basket.API
{
  public class Startup
  {
    private BasketAppSettings _appSettings;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      _appSettings = new BasketAppSettings(configuration);
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = _appSettings.CacheSettings;
      });
      services.AddScoped<IBasketRepository, BasketRepository>();
      services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => options.Address = _appSettings.DiscountUrl);
      services.AddScoped<DiscountGrpcService>();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
