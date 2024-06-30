using Simple.Infra.Database;
using Simple.Infra.DomainEvents;

namespace Simple.Infra.Behaviours;

internal class TransactionalBehaviour<TRequest, TResponse>(Db db, IDomainEventDispatcher dispatcher, ILogger<TransactionalBehaviour<TRequest, TResponse>> log)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().FullName!.Split(".").Last();

        return IsQuery(name)
            ? await next()
            : await HandleCommand(next, name, cancellationToken);
    }

    private static bool IsQuery(string name) =>
        name.Contains("query", StringComparison.InvariantCultureIgnoreCase);

    private async Task<TResponse> HandleCommand(RequestHandlerDelegate<TResponse> next, string name, CancellationToken cancellationToken)
    {
        log.LogDebug($"Start Transaction: {name}");
        await db.Database.BeginTransactionAsync(cancellationToken);
        var response = await next();
        await dispatcher.Publish();
        await db.SaveChangesAsync(cancellationToken);
        await db.Database.CommitTransactionAsync(cancellationToken);
        log.LogDebug($"End Transaction: {name}");
        return response;
    }
}