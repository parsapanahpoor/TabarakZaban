using System.Linq.Expressions;
using Framework.Domain.Abstraction.ReadParameters;

namespace Framework.Persistence.Shared
{
    public class ParameterizeQueryService
    {
        public static IQueryable<TEntity> ApplyOrderBy<TEntity>(IQueryable<TEntity> query, List<SortDescriptor> sortItems)
        {
            var counter = 0;
            sortItems.ForEach(sortItem =>
            {
                query = ApplyOrderBy(query, sortItem, counter == 0);
                counter += 1;
            });
            return query;
        }

        private static IQueryable<TEntity> ApplyOrderBy<TEntity>(IQueryable<TEntity> query, SortDescriptor sortItem, bool isInitial)
        {
            var entityType = typeof(TEntity);
            var propertyInfo = entityType.GetProperty(sortItem.Field);
            if (propertyInfo is null)
                return query;

            var parameterExpression = Expression.Parameter(entityType, "p");
            Expression memberExpression = Expression.Property(parameterExpression, propertyInfo);

            var delegateType = typeof(Func<,>).MakeGenericType(entityType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(delegateType, memberExpression, parameterExpression);

            string methodName;
            if (!isInitial && query is IOrderedQueryable<TEntity>)
                methodName = sortItem.Dir == SortDirectionEnum.Asc ? "ThenBy" : "ThenByDescending";
            else
                methodName = sortItem.Dir == SortDirectionEnum.Asc ? "OrderBy" : "OrderByDescending";

            var result = (IOrderedQueryable<TEntity>?) typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType)
                .Invoke(null, new object[] {query, lambda});
            return result ?? query;
        }
    }
}