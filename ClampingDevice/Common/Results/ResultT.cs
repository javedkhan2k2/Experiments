namespace ClampingDevice.Common.Results;

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) =>
        new(value, true, Error.None);

    public static new Result<T> Failure(Error error) =>
        new(default, false, error);

    public Result<U> Bind<U>(Func<T, Result<U>> func)
    {
        if (IsFailure) return Result.Failure<U>(Error);

        return func(Value!);
    }

    public Result<U> Map<U>(Func<T, U> func)
    {
        if (IsFailure) return Result.Failure<U>(Error);

        return Result.Success(func(Value!));
    }

    public U Match<U>(Func<T, U> onSuccess, Func<Error, U> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error);
    }
}
