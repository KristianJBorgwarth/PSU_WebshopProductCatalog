using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Behaviour;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null) return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return GenerateErrorResult(validationResult) as TResponse;
        }

        return await next();
    }

    private static Result GenerateErrorResult(ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .Select(e => Errors.General.UnspecifiedError(e.ErrorMessage))
            .ToList();

        var combinedError = Result.Combine(errors.Select(Result.Fail).ToArray());
        return combinedError;
    }

}