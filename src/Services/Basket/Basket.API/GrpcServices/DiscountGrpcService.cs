using System;
using System.Threading.Tasks;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService)); ;
        }

        public async Task<CouponModel> GetDisCount(string sku)
        {
            var discountRequest = new GetDiscountRequest { Sku = sku };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
