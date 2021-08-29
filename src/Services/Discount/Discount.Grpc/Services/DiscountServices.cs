using System;
using Grpc.Core;
using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Entities;
using System.Threading.Tasks;
using Discount.Grpc.Repositories;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
  public class DiscountServices : DiscountProtoService.DiscountProtoServiceBase
  {   
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountServices> _logger;    

    public DiscountServices(ILogger<DiscountServices> logger, IMapper mapper,IDiscountRepository repository)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
      var coupon = await _repository.GetDiscount(request.ProductName);
      if (coupon == null)
      {
        throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
      }
      _logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
      var coupon = _mapper.Map<Coupon>(request.Coupon);

      await _repository.CreateDiscount(coupon);
      _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
      var coupon = _mapper.Map<Coupon>(request.Coupon);

      await _repository.UpdateDiscount(coupon);
      _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
      var deleted = await _repository.DeleteDiscount(request.ProductName);
      var response = new DeleteDiscountResponse
      {
        Success = deleted
      };

      return response;
    }
  }
}
