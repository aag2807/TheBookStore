using Core.Boundaries.Persistance.Util;

namespace Core.Boundaries.Persistance;

public interface IUserRepository
{
     /// <summary>
     ///  Verifies validity of a user's credentials
     /// </summary>
     /// <param name="user">An instance of a core domain entity <see cref="Core.User.User"/></param>
     /// <returns>A valid domain entity of <see cref="Core.User.User"/></returns>
     public Task<Core.User.User> GetUserByUsernameAndPassword(Core.User.User user);
     
     /// <summary>
     /// Persists an existing user to the database
     /// </summary>
     /// <param name="user"> A valid instance of <see cref="Core.User"/></param>
     public Task AddUser(Core.User.User user);
     
     /// <summary>
     /// Get's all the existing users from the database
     /// </summary>
     /// <returns> A valid collection of <see cref="Core.User.User"/></returns>
     public Task<IEnumerable<Core.User.User>> GetAllUsers();

     /// <summary>
     /// Get's a single user by criteria
     /// </summary>
     /// <param name="criteria">A valid <see cref="Criteria"/></param>
     /// <returns>A valid <see cref="Core.User.User"/></returns>
     Task<Core.User.User> GetUserByCriteria(Criteria criteria);
}