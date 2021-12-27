using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedDataAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if(!orderContext.Order.Any())
            {
                orderContext.Order.AddRange(GetPreconfigureOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed databads associated with contex {DbContextName}", typeof(OrderContext).Name);  
            }
        }

        private static IEnumerable<Order> GetPreconfigureOrders()
        {
            return new List<Order>()
            {
                new Order(){ UserName = "Asi", FirstName= "Jal", LastName = "Ma", EmailAddress = "asi@gmail.com", AddressLine = "Sagor", Country ="BD", TotalPrice = 399 }
            };
        }
    }
}
