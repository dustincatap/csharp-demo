using Erni.University.Exceptions;
using Erni.University.Models;
using Erni.University.Repositories;
using Erni.University.Services;
using Microsoft.Extensions.Logging;

namespace Erni.University;

class Program
{
    static void Main(string[] args)
    {
        var programLogger = LoggerProvider.CreateLogger<Program>();
        var userServiceLogger = LoggerProvider.CreateLogger<UserService>();
        var userService = new UserService(new UserRepository(), userServiceLogger);
        var getUserResult = userService.GetUsers();

        switch (getUserResult)
        {
            case Success<IEnumerable<User>> { Value: var users }:
                users.ToList().ForEach(u => programLogger.LogInformation("User: {user}", u));
                break;

            case Failure<IEnumerable<User>> { Exception: NoUsersFoundException exception }:
                programLogger.LogError(exception, "An error occurred: {message}", exception.Message);
                break;

            case Failure<IEnumerable<User>>
            {
                Exception: UserNotFoundException { ErrorCode: "USER_NOT_FOUND" } exception
            }:
                programLogger.LogWarning(exception, "An error occurred: {message}", exception.Message);
                break;
        }
    }
}