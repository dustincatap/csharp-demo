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

    public IEnumerable<User> GetUsers()
    {
        return DefaultUsers;
    }
}