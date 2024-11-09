namespace Framework.Domain.Abstraction;

public interface IAuditable
{
    public DateTimeOffset? CreatedDateTime { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedByIp { get; set; }
    
    public DateTimeOffset? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
    public string? ModifiedByIp { get; set; }
}