namespace Simple.Domain.Common;

public record Page(int PageNumber, int PageSize)
{
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}