using Course.Domain.CourseCategory.Aggregate; 

namespace Course.Domain.CourseCategory.Validation;

public class CourseCategoryAggregateValidator : AbstractValidator<CourseCategoryAggregate>
{
    public CourseCategoryAggregateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(2, 200);
    }
}
