namespace Framework.Domain.Abstraction.ReadParameters;

public interface IQueryParameter  
{
    public List<SortDescriptor>? Sort { get; set; } 
    public int? Take { get; set; }
    public int? Skip { get; set; }
}