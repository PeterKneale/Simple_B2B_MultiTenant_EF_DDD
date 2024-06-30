namespace Simple.App;

public record PaginatedResult<T>(IReadOnlyList<T> Items, int Total, int PageNumber, int PageSize);