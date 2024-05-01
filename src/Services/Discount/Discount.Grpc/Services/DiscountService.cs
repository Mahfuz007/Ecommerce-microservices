using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService (DiscountContext _dbContet, ILogger<DiscountService> _logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContet.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Name);
        if (coupon == null)
        {
            coupon = new Coupon { ProductName = "No Discount", Description = "NO discount available for this product.", Amount = 0 };
        }

        _logger.LogInformation($"Discout Found for ProductName: {coupon.ProductName}, Description: {coupon.Description}, Amount: {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        _dbContet.Add(coupon);
        await _dbContet.SaveChangesAsync();

        _logger.LogInformation($"Discout Successfully created for ProductName: {coupon.ProductName}, Description: {coupon.Description}, Amount: {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        _dbContet.Update(coupon);
        await _dbContet.SaveChangesAsync();

        _logger.LogInformation($"Discout Successfully Updated for ProductName: {coupon.ProductName}, Description: {coupon.Description}, Amount: {coupon.Amount}");

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContet.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if(coupon is null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        _dbContet.Remove(coupon);
        await _dbContet.SaveChangesAsync();

        _logger.LogInformation($"Discout Successfully Deleted for ProductName: {request.ProductName}");

        return new DeleteDiscountResponse { IsSuccess = true };
    }
}