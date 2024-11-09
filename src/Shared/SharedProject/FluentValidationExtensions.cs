using FluentValidation.Results;

namespace SharedProject;

public static class FluentValidationExtensions
{
    public static Result<T> FromValidation<T>(this Result<T> result, ValidationResult validationResult)
        => Result<T>.Failure(validationResult.Errors.Select(p => new Error(p.ErrorCode, p.ErrorMessage)));

    public static Result FromValidation(this Result result, ValidationResult validationResult)
        => Result.Failure(validationResult.Errors.Select(p => new Error(p.ErrorCode, p.ErrorMessage)));
}