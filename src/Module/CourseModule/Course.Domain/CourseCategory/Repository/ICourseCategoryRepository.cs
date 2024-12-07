using Course.Domain.CourseCategory.Aggregate;
using Framework.Domain.Abstraction;

namespace Course.Domain.CourseCategory.Repository;

public interface ICourseCategoryRepository : IRepository<CourseCategoryAggregate>
{
    Task<string?> GetPathById(Guid CourseCategoryId, CancellationToken cancellationToken); 
    Task<bool> TitleDuplicationChecker(Guid parentId, string title, CancellationToken cancellationToken);
    Task<Guid?> GetParentIdById(Guid CourseCategoryId, CancellationToken cancellationToken);
    Task<IEnumerable<CourseCategoryAggregate>> GetChildrenAsync(Guid parentId, CancellationToken cancellationToken);
}