
namespace Framework.Domain.Shared.Category.BaseAggregate;

public abstract class CategoryBaseAggregate : BaseAggregateRoot<Guid>
{
    public virtual Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? ParentPath { get; set; }
    public string? Description { get; set; }

    public string Path => string.IsNullOrEmpty(ParentPath) ?
    Id.ToString() :
    string.Join('.', ParentPath, Id);

    public bool IsSuperset()
        => Path.Equals(Id) ||
           Path.StartsWith(Id + ".");
}