using Core.User.Aggregates;

namespace Core.User.Service;

public interface IUserService
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user">A core entity of <see cref="Core.User"/></param>
    public Task CreateUser(User user);
    
    /// <summary>
    /// verifies the credentials of a user to log him in
    /// </summary>
    /// <param name="loginUser">An aggregate of type <see cref="LoginUser"/></param>
    /// <returns>A valid instance of <see cref="Core.User"/></returns>
    public Task<User> LoginUser(LoginUser loginUser);
}