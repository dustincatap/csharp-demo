using Erni.University.Exceptions;
using Erni.University.Models;
using Erni.University.Repositories;
using Microsoft.Extensions.Logging;

namespace Erni.University.Services;

public class UserService
{
    private readonly IRepository<User> _userRepository;
    private readonly ILogger<UserService> _logger;
    
    public UserService(IRepository<User> userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public Result<IEnumerable<User>> GetUsers()
    {
        var users = _userRepository.GetAll().ToList();

        if (users.Count == 0)
        {
            _logger.LogError("No users found");
            
            return new Failure<IEnumerable<User>>(new NoUsersFoundException());
        }

        _logger.LogInformation("Users found: {users}", users);
        
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
                _logger.LogWarning("User with email {email} not found", email);
                
                return new Failure<User>(new UserNotFoundException(email, "USER_NOT_FOUND"));
            }
            
            _logger.LogInformation("User found: {user}", user);

            return new Success<User>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {message}", ex.Message);
            
            return new Failure<User>(ex);
        }
    }
}