namespace SharedProject;

public class Result
{
    protected Result(IEnumerable<Error>? errors)
    {
        IsSuccess = false;
        Errors = errors;
    }

    protected Result()
    {
        IsSuccess = true;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IEnumerable<Error>? Errors { get; set; }
    public bool HasError<T>() where T : Error => Errors?.Any(p => p.GetType() == typeof(T)) == true;
    public static Result Success => new();
    public static Result Failure(params Error[] error) => new(error);
    public static Result Failure(IEnumerable<Error> error) => new(error);

    public static Result Failure(string errorMessage, string? errorCode = null) =>
        Failure(new Error(errorCode ?? string.Empty, errorMessage));

    public static implicit operator bool(Result result) => result.IsSuccess;
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    private Result(TValue? value) => _value = value;

    private Result(IEnumerable<Error>? errors) : base(errors)
    {
    }

    public new static Result<TValue> Success(TValue? value) => new(value);
    public new static Result<TValue> Failure(IEnumerable<Error>? errors) => new(errors);
    public new static Result<TValue> Failure(params Error[]? errors) => new(errors);
    
    // ReSharper disable once UnusedMember.Global
    public static Result<TValue> Failure<T>() where T : Error, new() => Failure(new T());
    public static Result<TValue> Failure(string errorMessage) => Failure(new Error(string.Empty, errorMessage));

    public TValue? Value => IsSuccess ? _value : throw new InvalidOperationException("The value of the failure result can't be accessed");

    public static implicit operator bool(Result<TValue> result) => result.IsSuccess;
}