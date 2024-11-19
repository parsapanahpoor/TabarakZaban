using Framework.Domain.Abstraction.ReadParameters;

namespace Framework.Application.Shared.Models;

public record QueryParameter : IQueryParameter
{
    public List<SortDescriptor>? Sort { get; set; } = [];
    public int? Take { get; set; }
    public int? Skip { get; set; }
}