namespace Erni.University;

using Erni.University.Services;

class Program
{
    static void Main(string[] args)
    {
        var userService = new UserService();
        var users = userService.GetUsers();
        users.ToList().ForEach(Console.WriteLine);
    }
}
