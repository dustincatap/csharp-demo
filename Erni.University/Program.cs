using Erni.University.Models;
using Erni.University.Services;

namespace Erni.University;

class Program
{
    static void Main(string[] args)
    {
        var userService = new UserService();
        var getUserResult = userService.GetUsers();

        switch (getUserResult)
        {
            case Success<IEnumerable<User>> { Value: var users }:
                users.ToList().ForEach(Console.WriteLine);
                break;

            case Failure<IEnumerable<User>> { Exception: NoUsersFoundException exception }:
                Console.WriteLine($"An error occurred: {exception.Message}");
                break;

            case Failure<IEnumerable<User>> { Exception: Exception exception }:
                Console.WriteLine($"An error occurred: {exception.Message}");
                break;
        }
    }

    static void Bar()
    {
        var userService = new UserService();
        var getUserResult = userService.GetUser("charlie@test.com");


        if (getUserResult is not Success<User> { Value: var user })
        {
            return;
        }

        Console.WriteLine(user);

        var getUsersResult = userService.GetUsers();

        if (getUsersResult is not Success<IEnumerable<User>> { Value: var users })
        {
            return;
        }

        users.ToList().ForEach(Console.WriteLine);
    }
}
