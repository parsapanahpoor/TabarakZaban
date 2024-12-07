using Framework.Domain.Shared;
using Framework.Domain.Shared.Category.BaseAggregate;

namespace Course.Domain.CourseCategory.Aggregate;

public class CourseCategoryAggregate : CategoryBaseAggregate
{
    public virtual CourseCategoryAggregate? Parent { get; set; }
    public virtual ICollection<CourseCategoryAggregate> ChildCategories { get; set; } = [];
}
