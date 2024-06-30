using Newtonsoft.Json;

namespace Simple.Infra.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<IMediator> log) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().FullName!.Split(".").Last();
        var body = JsonConvert.SerializeObject(request);
        try
        {
            return IsQuery(name)
                ? await HandleQuery(next, name, body)
                : await HandleCommand(next, name, body);
        }
        catch (Exception e)
        {
            log.LogError(e, "Error Executing: {Name} - {Body}", name, body);
            throw;
        }
    }

    private static bool IsQuery(string name) =>
        name.Contains("query", StringComparison.InvariantCultureIgnoreCase);

    private async Task<TResponse> HandleQuery(RequestHandlerDelegate<TResponse> next, string name, string body)
    {
        log.LogInformation($"Start Query: {name} - {body}");
        var result = await next();
        log.LogInformation($"End Query {name} - {body}");
        return result;
    }

    private async Task<TResponse> HandleCommand(RequestHandlerDelegate<TResponse> next, string name, string body)
    {
        log.LogInformation($"Start Command: {name} - {body}");
        var response = await next();
        log.LogInformation($"End Command: {name} - {body}");
        return response;
    }
}