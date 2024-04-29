namespace Core.Boundaries.Persistance;

public interface IUserRepository
{
     /// <summary>
     ///  Verifies validity of a user's credentials
     /// </summary>
     /// <param name="user">An instance of a core domain entity <see cref="Core.User.User"/></param>
     /// <returns>A valid domain entity of <see cref="Core.User.User"/></returns>
     public Task<Core.User.User> GetUserByUsernameAndPassword(Core.User.User user);
     
     public Task AddUser(Core.User.User user);
     
     public Task<IEnumerable<Core.User.User>> GetAllUsers();
}