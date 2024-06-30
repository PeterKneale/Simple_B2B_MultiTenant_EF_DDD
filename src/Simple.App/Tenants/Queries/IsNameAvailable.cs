using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;

namespace Simple.App.Tenants.Queries;

public static class IsNameAvailable
{
    public record Query(string TenantName) : IRequest<Result>;

    public record Result(bool IsNameValid, bool? IsNameAvailable = null, string? Suggestion = null, bool? IsSuggestionAvailable = null)
    {
        public static Result NameUnavailable() => new(true, false);

        public static Result NameAvailable() => new(true, true);

        public static Result NameInvalidButSuggestionAvailable(string suggestion) => new(false, null, suggestion, true);

        public static Result NameInvalidAndSuggestionUnavailable(string suggestion) => new(false, null, suggestion, false);
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.TenantName).NotEmpty();
        }
    }

    public class Handler(IReadRepository<Tenant> tenants) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var value = query.TenantName;
            var valid = TenantName.IsValidName(value);
            return valid
                ? await NameIsValid(value, cancellationToken)
                : await NameIsInvalid(value, cancellationToken);
        }

        private async Task<Result> NameIsValid(string value, CancellationToken cancellationToken)
        {
            var name = TenantName.Create(value);
            var result = await tenants.SingleOrDefaultAsync(new TenantByNameSpec(name), cancellationToken);
            return result == null
                ? Result.NameAvailable()
                : Result.NameUnavailable();
        }

        private async Task<Result> NameIsInvalid(string value, CancellationToken cancellationToken)
        {
            var name = TenantName.GetSuggestion(value);
            var result = await tenants.SingleOrDefaultAsync(new TenantByNameSpec(name), cancellationToken);
            return result == null
                ? Result.NameInvalidButSuggestionAvailable(name)
                : Result.NameInvalidAndSuggestionUnavailable(name);
        }
    }
}