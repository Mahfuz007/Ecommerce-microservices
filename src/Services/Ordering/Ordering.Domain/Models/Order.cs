using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Order : Aggregrator<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName Name { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment PaymentDetails { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Quantity * x.Price);
        private set { }
    }

    public static Order Create(OrderId orderId, OrderName name, CustomerId customerId, Address shippingAddress, Address billingAddress, Payment paymentDetails)
    {
        var order = new Order
        {
            Id = orderId,
            Name = name,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            PaymentDetails = paymentDetails,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreateEvent(order));

        return order;
    }

    public void Update(OrderName name, Address shippingAddress, Address billingAddress, Payment paymentDetails, OrderStatus status)
    {
        Name = name;
        ShippingAddress = ShippingAddress;
        BillingAddress = billingAddress;
        PaymentDetails = paymentDetails;
        Status = status;

        AddDomainEvent(new OrderUpdateEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}