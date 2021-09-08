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
using MassTransit;

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
      // Redis Configuration
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = _appSettings.CacheSettings;
      });

      // General Configuration
      services.AddScoped<IBasketRepository, BasketRepository>();
      services.AddAutoMapper(typeof(Startup).Assembly);

      // Grpc Configuration
      services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => options.Address = _appSettings.DiscountUrl);
      services.AddScoped<DiscountGrpcService>();

      // MassTransit-RabbitMQ Configuration
      services.AddMassTransit(config =>
      {
        config.UsingRabbitMq((ctx, cfg) =>
        {
          cfg.Host(_appSettings.HostAddress);
        });
      });

      services.AddMassTransitHostedService();      

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
