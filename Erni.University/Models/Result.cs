namespace Erni.University.Models;


public abstract class Result<T>
{
    public bool IsSuccess { get; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}

public sealed class Success<T> : Result<T>
{
    public T Value { get; }

    public Success(T value) : base(true)
    {
        Value = value;
    }
}

public sealed class Failure<T> : Result<T>
{
    public Exception Exception { get; }

    public Failure(Exception exception) : base(false)
    {
        Exception = exception;
    }
}