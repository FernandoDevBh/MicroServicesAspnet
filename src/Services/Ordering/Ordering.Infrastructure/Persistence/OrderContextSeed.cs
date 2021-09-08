using System.Linq;
using System.Threading.Tasks;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Persistence
{
  public class OrderContextSeed
  {
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
      if (!orderContext.Orders.Any())
      {
        orderContext.Orders.AddRange(GetPreconfiguredOrders());
        await orderContext.SaveChangesAsync();
        logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
      }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
      return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Fernando", LastName = "Ferreira", EmailAddress = "teste@gmail.com", AddressLine = "Bahcelievler", Country = "Brazil", TotalPrice = 350 }
            };
    }
  }
}