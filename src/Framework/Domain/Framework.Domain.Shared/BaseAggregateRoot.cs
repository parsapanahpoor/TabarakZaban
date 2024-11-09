namespace Framework.Domain.Shared;

public abstract class BaseAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditable
    where TKey : notnull
{
    protected BaseAggregateRoot()
    {
    }

    protected BaseAggregateRoot(TKey id) : base(id)
    {
    }

    public DateTimeOffset? CreatedDateTime { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedByIp { get; set; }
    public DateTimeOffset? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
    public string? ModifiedByIp { get; set; }
}