using System;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatebase<TContext>(this IHost host,
                                                      Action<TContext,IServiceProvider> action,
                                                      int? retry = 0) where TContext : DbContext
        {
            int retryValue = retry.Value;

            using(var scop = host.Services.CreateScope())
            {
                var provider = scop.ServiceProvider;
                var services = provider.GetRequiredService<ILogger<TContext>>();
                var context = provider.GetService<TContext>();

                try
                {
                    InvokeSeeder(action, context, provider);
                }
                catch(SqlException ex)
                {

                    if(retryValue < 50)
                    {
                        retryValue++;
                        Thread.Sleep(2000);
                        MigrateDatebase<TContext>(host, action, retryValue);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> action, TContext context, IServiceProvider provider) where TContext : DbContext
        {
            context.Database.Migrate();
            action(context, provider);
        }
    }
}
