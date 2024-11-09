namespace Framework.Domain.Abstraction.ReadParameters;

public record SortDescriptor(string Field, SortDirectionEnum Dir = SortDirectionEnum.Asc)
{
    public SortDirectionEnum Dir { get; set; } = Dir;
    public string Field { get; set; } = Field; 
}

public enum SortDirectionEnum
{
    Asc,
    Desc
}