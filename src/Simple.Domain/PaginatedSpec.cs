using Ardalis.Specification;

namespace Simple.Domain;

public class PaginatedSpec<T> : Specification<T>
{
    public PaginatedSpec(int page, int pageSize)
    {
        if (page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than 0");
        }

        switch (pageSize)
        {
            case < 1:
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0");
            case > 100:
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be less than or equal to 100");
        }

        Skip = (page - 1) * pageSize;
        Take = pageSize;
    }

    protected int Skip { get; }
    protected int Take { get; }
}