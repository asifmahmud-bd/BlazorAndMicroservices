using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scop = host.Services.CreateScope())
            {
                var services = scop.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var loger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    loger.LogInformation("Migrating prstgresql discount database");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                              Sku VARCHAR(30) NOT NULL,
                                                              ProductName VARCHAR(30) NOT NULL,
                                                              Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (Sku, ProductName, Amount) VALUES ('IP02112101', 'IPhone X', '50');";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon (Sku, ProductName, Amount) VALUES ('XM01112301', 'Xiaomi Mi 9', '88');";
                    command.ExecuteNonQuery();

                    loger.LogInformation("Migration successfull.");
                }
                catch(NpgsqlException ex)
                {
                    loger.LogInformation($"An error occurred when migration prstgresql discount database.{ex}");

                    if(retryForAvailability < 30)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }
}
