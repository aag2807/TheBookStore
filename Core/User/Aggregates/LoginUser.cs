namespace Core.User.Aggregates;

public sealed class LoginUser
{
    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
    
    public Core.User.User ToCoreUser()
    {
        return new Core.User.User
        {
            Username = Username,
            Password = Password
        };
    }
}