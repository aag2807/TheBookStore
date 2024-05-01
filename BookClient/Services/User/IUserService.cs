namespace BookClient.Services.User;

public interface IUserService
{
    /// <summary>
    /// Logs in the user and returns a Core user entity
    /// </summary>
    /// <param name="username">A valid string primitive that represents the users username</param>
    /// <param name="password">A valid string primitive that represents the users password</param>
    /// <returns>A valid <see cref="Core.User.User"/> Entity</returns>
    public IObservable<Core.User.User> LoginUser(string username, string password);
}