using Course.Domain.CourseCategory.Aggregate;
using Course.Domain.CourseCategory.Model;
using Framework.Domain.Abstraction;

namespace Course.Domain.CourseCategory.Service;

public interface ICourseCategoryService :
    ISimpleBaseService<CourseCategoryAggregate, CourseCategoryCreateModel>;