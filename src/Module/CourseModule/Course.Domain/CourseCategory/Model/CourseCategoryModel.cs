namespace Course.Domain.CourseCategory.Model;

public record CourseCategoryModel
{
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}