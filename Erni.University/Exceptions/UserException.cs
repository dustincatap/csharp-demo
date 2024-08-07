namespace Erni.University.Exceptions;

public class UserException : Exception
{
    public string ErrorCode { get; } = string.Empty;

    public UserException(string message) : base(message)
    {
    }

    public UserException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public UserException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class NoUsersFoundException : UserException
{
    public NoUsersFoundException() : base("No users found")
    {
    }
}

public class UserNotFoundException : UserException
{
    public string Email { get; }

    public UserNotFoundException(string email) : base($"User with email {email} not found")
    {
        Email = email;
    }

    public UserNotFoundException(string email, string errorCode) : base($"User with email {email} not found", errorCode)
    {
        Email = email;
    }
}