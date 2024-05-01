namespace Core.User.Aggregates;

public sealed class LoginUser
{
    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
    
    public User ToCoreUser()
    {
        return new User
        {
            Username = Username,
            Password = Password
        };
    }
}