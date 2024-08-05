namespace Erni.University.Models;


public abstract class Result<T>
{
    public bool IsSuccess { get; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}

public abstract class Result
{
    public bool IsSuccess { get; }

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}

public sealed class Success : Result
{
    public Success() : base(true)
    {
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

public sealed class Failure : Result
{
    public Exception Exception { get; }

    public Failure(Exception exception) : base(false)
    {
        Exception = exception;
    }
}