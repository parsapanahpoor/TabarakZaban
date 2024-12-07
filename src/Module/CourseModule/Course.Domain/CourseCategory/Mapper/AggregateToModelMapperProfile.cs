using Course.Domain.CourseCategory.Aggregate;
using Course.Domain.CourseCategory.Model;

namespace Course.Domain.CourseCategory.Mapper;

public class AggregateToModelMapperProfile : Profile
{
    public AggregateToModelMapperProfile()
    {
        CreateMap<CourseCategoryAggregate, CourseCategoryCreateModel>().ReverseMap();
        CreateMap<CourseCategoryAggregate, CourseCategoryModel>().ReverseMap();
    }
}