using System;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscountAsync(request.Sku);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product named {coupon.ProductName} is not found"));
            }

            _logger.LogInformation($"Discount product named is {coupon.ProductName}, Sku = {coupon.Sku} and discount price = {coupon.Amount}");

            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var isCreated = await _repository.CreateDiscountAsync(coupon);

            if(isCreated)
            {
                _logger.LogInformation($"Discount is successfully created for product name = {coupon.ProductName}");

                var couponMadel = _mapper.Map<CouponModel>(coupon);

                return couponMadel;
            }

            _logger.LogInformation($"Discount is not created for product name = {coupon.ProductName}");

            throw new RpcException(new Status(StatusCode.Aborted, $"Discount is not created for product name = {coupon.ProductName}"));
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var isUpdated = await _repository.UpdateDiscountAsync(coupon);

            if (isUpdated)
            {
                _logger.LogInformation($"Coupon is successfully updated for product name = {coupon.ProductName}");

                var couponMadel = _mapper.Map<CouponModel>(coupon);

                return couponMadel;
            }

            _logger.LogInformation($"Coupon is not updated with product name = {coupon.ProductName}");

            throw new RpcException(new Status(StatusCode.Aborted, $"Coupon is not updated with product name = {coupon.ProductName}"));

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var isDeleted = await _repository.DeleteDiscountAsync(request.Sku);

            var response = new DeleteDiscountResponse
            {
                Success = isDeleted
            };

            if (isDeleted)
            {
                _logger.LogInformation($"Coupon is successfully delete with sku = {request.Sku}");

                return response;
            }

            _logger.LogInformation($"Coupon is not delete with sku = {request.Sku}");

            return response;
        }
    }
}
