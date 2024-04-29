using Core.User.Aggregates;

namespace Core.User.Service;

public interface IUserService
{
    public Task CreateUser(User user);
    
    public Task<User> LoginUser(LoginUser loginUser);
}