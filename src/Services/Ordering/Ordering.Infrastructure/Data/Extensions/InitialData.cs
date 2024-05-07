namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("afea5876-6bb6-4d06-a60b-76f2850754ee")), "Mahfuz","mahfuz@yopmail.com"),
        Customer.Create(CustomerId.Of(new Guid("851cc746-9f37-4fbe-b36e-ed34756bce09")), "Shovon","shovon@yopmail.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf1")), "Iphone 15 Pro Max", 150000),
        Product.Create(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf2")), "Samsung S24 Ultra", 116000),
        Product.Create(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf3")), "Iphone 24 Pro Max", 140000),
        Product.Create(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf4")), "Samsung S23 Ultra", 98000)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Abdullah Al", "Mahfuz", "aamahfuz2@gmail.com", "Housing c-66", "Kushtia", "kushtia", "7000", "Bangladesh");
            var address2 = Address.Of("Mr", "Shovon", "shovon@yopmail.com", "Midas center", "dhaka", "dhaka", "1205", "Bangladesh");

            var payment1 = Payment.Of("Mahfuz", "5555555555554444", "355", "12/28", 1);
            var payment2 = Payment.Of("Shovon", "8885555555554444", "222", "06/30", 2);

            var order1 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            OrderName.Of("ORD_1"),
                            CustomerId.Of(new Guid("afea5876-6bb6-4d06-a60b-76f2850754ee")),
                            shippingAddress: address1,
                            billingAddress: address1,
                            payment1);
            order1.Add(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf1")), 2, 150000);
            order1.Add(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf4")), 1, 98000);

            var order2 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            OrderName.Of("ORD_2"),
                            CustomerId.Of(new Guid("851cc746-9f37-4fbe-b36e-ed34756bce09")),
                            shippingAddress: address2,
                            billingAddress: address2,
                            payment2);
            order2.Add(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf2")), 1, 116000);
            order2.Add(ProductId.Of(new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf3")), 2, 140000);

            return new List<Order> { order1, order2 };
        }
    }
}
