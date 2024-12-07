namespace Framework.Domain.Shared.Category.Model;

public abstract record CategoryBaseModel
{
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public abstract record CategoryUpdateBaseModel
{
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
