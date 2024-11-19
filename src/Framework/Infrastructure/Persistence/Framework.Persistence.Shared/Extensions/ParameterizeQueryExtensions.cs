using Framework.Domain.Abstraction.ReadParameters;

namespace Framework.Persistence.Shared.Extensions
{
    public static class ParameterizeQueryExtensions
    {
        public static IQueryable<TEntity> ApplyOrderBy<TEntity>(this IQueryable<TEntity> query, List<SortDescriptor> sortItems)
            => ParameterizeQueryService.ApplyOrderBy(query, sortItems);
    }
}