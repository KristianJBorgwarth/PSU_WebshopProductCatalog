using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Behaviour;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    private readonly IValidator<TRequest>? _validator;
    private readonly ILogger _logger;

    public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IValidator<TRequest>? validator = null)
    {
        _logger = logger;
        _logger.LogWarning("validationLogger called");
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