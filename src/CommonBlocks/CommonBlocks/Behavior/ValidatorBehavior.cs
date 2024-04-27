using CommonBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace CommonBlocks.Behavior;

public class ValidatorBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var errors = validationResult.SelectMany(v => v.Errors).Where(v => v != null).ToList();
            if(errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return await next();
    }
}