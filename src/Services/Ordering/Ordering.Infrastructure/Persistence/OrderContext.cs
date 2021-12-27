using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;


namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public OrderContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public OrderContext(DbContextOptions<DbContext> context)
            :base(context)
        {
        }

        public DbSet<Order> Order { get; set; }

        //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = _configuration.GetConnectionString("OrderingConnectionString");
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("OrderingConnectionString"));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<EntityBase >())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        entry.Entity.CreateBy = "Asi";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModefiedBy = "Asi";
                        break;
                }
            }

            return base.SaveChangesAsync();

        }
    }
}
