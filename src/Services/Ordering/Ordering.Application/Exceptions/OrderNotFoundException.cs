using CommonBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(string message) : base("Order",message)
    {
    }
}