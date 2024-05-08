namespace Ordering.Application.Extensions;
public static class OrderExtensions
{
    public static IEnumerable<OrderDto> OrderToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => DtoFromOrder(order)).ToList();
    }

    public static OrderDto OrderToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    public static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                orderId: order.Id.Value,
                name: order.Name.Value,
                customerId: order.CustomerId.Value,
                shippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.Email, order.ShippingAddress.AddressLine, order.ShippingAddress.City, order.ShippingAddress.State, order.ShippingAddress.ZipCode, order.ShippingAddress.Country),
                billingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.Email, order.BillingAddress.AddressLine, order.BillingAddress.City, order.BillingAddress.State, order.BillingAddress.ZipCode, order.BillingAddress.Country),
                paymentDetails: new PaymentDto(order.PaymentDetails.Name, order.PaymentDetails.CardNumber, order.PaymentDetails.CVV, order.PaymentDetails.ExpiryDate, order.PaymentDetails.PaymentMethod),
                status: order.Status,
                orderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
            );
    }
}
