using MediatR;
namespace CommonBlocks.CQRS;

public interface IQuery : IQuery<Unit>
{

}
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

