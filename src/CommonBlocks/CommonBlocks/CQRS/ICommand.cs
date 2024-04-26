using MediatR;
namespace CommonBlocks.CQRS;

public interface ICommand : ICommand<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}