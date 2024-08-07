using Erni.University.Exceptions;
using Erni.University.Models;
using Erni.University.Repositories;

namespace Erni.University.Services;

public class UserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public Result<IEnumerable<User>> GetUsers()
    {
        var users = _userRepository.GetAll().ToList();

        if (users.Count == 0)
        {
            return new Failure<IEnumerable<User>>(new NoUsersFoundException());
        }

        return new Success<IEnumerable<User>>(users);
    }

    public Result<User> GetUser(string email)
    {
        try
        {
            var users = _userRepository.GetAll().ToList();
            var user = users.FirstOrDefault(u => u.Email == email);

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
}