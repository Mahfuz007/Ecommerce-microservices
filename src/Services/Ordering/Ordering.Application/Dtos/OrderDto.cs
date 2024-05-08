using Ordering.Domain.Enums;
namespace Ordering.Application.Dtos;

public record OrderDto (
        Guid orderId, 
        string name, 
        Guid customerId, 
        AddressDto shippingAddress, 
        AddressDto billingAddress, 
        PaymentDto paymentDetails,
        OrderStatus status,
        List<OrderItemDto> orderItems
    );