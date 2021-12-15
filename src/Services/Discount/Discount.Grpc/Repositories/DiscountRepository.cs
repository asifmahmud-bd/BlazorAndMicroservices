using System;
using System.Threading.Tasks;
using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var isInserted = await connection.ExecuteAsync("INSERT INTO Coupon (Sku, ProductName, Amount) VALUES (@Sku, @ProductName, @Amount)",
                                                            new { Sku = coupon.Sku, ProductName = coupon.ProductName, Amount = coupon.Amount });

            if (isInserted == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDiscountAsync(string sku)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var isDeletedRow = await connection.ExecuteAsync("DELETE FROM Coupon WHERE Sku = @Sku", new { Sku = sku });

            if (isDeletedRow == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<Coupon> GetDiscountAsync(string sku)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE Sku = @Sku", new { Sku = sku });

            if (coupon == null)
            {
                return new Coupon { ProductName = "", Sku = "", Amount = 0 };
            }

            return coupon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var isUpdated = await connection.ExecuteAsync("UPDATE Coupon SET Sku=@Sku, ProductName=@ProductName, Amount=@Amount WHERE Id=@Id",
                                                            new { Id = coupon.Id, Sku = coupon.Sku, ProductName = coupon.ProductName, Amount = coupon.Amount});

            if (isUpdated == 0)
            {
                return false;
            }

            return true;
        }
    }
}
