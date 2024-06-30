using FluentValidation;

namespace Simple.Infra.Behaviours;

internal class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logs)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().FullName!.Split(".").Last();

        if (!validators.Any())
        {
            logs.LogWarning("{Name} has no validators", name);
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = results
            .Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count == 0)
        {
            logs.LogDebug("{Name} is valid", name);
            return await next();
        }

        var errors = string.Join(",", failures.Select(x => x.ErrorMessage));
        logs.LogError("{Name} Failed validation {@Request} {errors}", name, request, errors);
        throw new Exception(errors);
    }
}