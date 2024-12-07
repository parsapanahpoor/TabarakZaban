using Course.Domain.CourseCategory.Aggregate;
using Course.Domain.CourseCategory.Model;
using Course.Domain.CourseCategory.Repository;
using Framework.Domain.Shared;
using SharedProject;

namespace Course.Domain.CourseCategory.Service;

public class CourseCategoryService(
    IMapper mapper,
    IValidator<CourseCategoryAggregate> validator,
    ICourseCategoryRepository CourseCategoryRepository) :
    SimpleBaseService<CourseCategoryAggregate, CourseCategoryCreateModel>(mapper, validator),
    ICourseCategoryService
{
    public override async Task Evaluation(CourseCategoryAggregate originalAggregate,
        CourseCategoryCreateModel data,
        CancellationToken cancellationToken)
         => originalAggregate.ParentPath = await GetParentPathString(data.ParentId, cancellationToken);

    public override async Task<Result<CourseCategoryAggregate>> ValidateAggregate(CourseCategoryAggregate aggregate,
          CancellationToken cancellationToken)
    {
        var validatorResult = await base.ValidateAggregate(aggregate, cancellationToken);
        if (validatorResult.IsFailure)
            return Result<CourseCategoryAggregate>
                .Failure(validatorResult.Errors ?? []);

        //ToDo : Move to Fluent Validation
        if (!string.IsNullOrEmpty(aggregate.ParentPath))
            if (aggregate.IsSuperset())
                return Result<CourseCategoryAggregate>
                .Failure(new Error("500", "Circular dependency detected. The CourseCategory cannot be its own ancestor."));

        if (aggregate.ParentId.HasValue)
            if (await CourseCategoryRepository.TitleDuplicationChecker(aggregate.ParentId.Value, aggregate.Title!, cancellationToken))
                return Result<CourseCategoryAggregate>
                    .Failure(new Error("500", "Title is duplicated."));

        return Result<CourseCategoryAggregate>.Success(aggregate);
    }

    private async Task<string?> GetParentPathString(Guid? parentId,
      CancellationToken cancellationToken)
    {
        if (!parentId.HasValue)
            return null;

        var parentPathString = await CourseCategoryRepository
            .GetPathById(parentId.Value, cancellationToken);

        return string.IsNullOrEmpty(parentPathString) ?
            parentId.ToString() :
            parentPathString + "." + parentId.ToString();
    }
}