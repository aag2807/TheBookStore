using BookClient.Services.Http;
using Core.User.Aggregates;

namespace BookClient.Services.User;

public sealed class UserService : BaseHttpClient, IUserService
{
    public UserService(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    IObservable<Core.User.User> IUserService.LoginUser( string username, string password)
    {
        LoginUser userData = new();
        userData.Username = username;
        userData.Password = password;

        return PostAsync<LoginUser, Core.User.User>("api/User/Login", userData);
    }
}