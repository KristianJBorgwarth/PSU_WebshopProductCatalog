using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    private readonly IValidator<TRequest>? _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IHttpContextAccessor httpContextAccessor, ILogger<ValidationBehavior<TRequest, TResponse>> logger, IValidator<TRequest>? validator = null)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null) return await next();

        var validationResult = await _validator.ValidateAsync(request, options => options.IncludeAllRuleSets(), cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for request {RequestPath}", _httpContextAccessor.HttpContext?.Request.Path);
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