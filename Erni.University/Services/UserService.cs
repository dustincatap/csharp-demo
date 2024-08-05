using Erni.University.Models;

namespace Erni.University.Services;

public class UserService
{
    private static readonly IList<User> DefaultUsers =
    [
        new User
        {
            Name = "Alice",
            Email = "alice@test.com"
        },
        new User
        {
            Name = "Bob",
            Email = "bob@test.com"
        },
        new User
        {
            Name = "Charlie",
            Email = "charlie@test.com"
        }
    ];

    public Result<IEnumerable<User>> GetUsers()
    {
        if (DefaultUsers.Count == 0)
        {
            return new Failure<IEnumerable<User>>(new NoUsersFoundException());
        }

        return new Success<IEnumerable<User>>(DefaultUsers);
    }

    public Result<User> GetUser(string email)
    {
        try
        {
            var user = DefaultUsers.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return new Failure<User>(new UserNotFoundException(email, "USER_NOT_FOUND"));
            }

            return new Success<User>(user);

        }
        catch (Exception ex)
        {
            return new Failure<User>(ex);
        }
    }

    public Result Foo()
    {
        return new Success();
    }
}

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