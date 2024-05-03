namespace Core.User;

public sealed class User
{
    public User()
    {
    }

    public User(string username, string password, string email, bool isAdmin, bool isBlocked)
    {
        Username = username;
        Password = password;
        Email = email;
        IsAdmin = isAdmin;
        IsBlocked = isBlocked;
    }

    public int UserId { get; set; }
    
    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
    
    public string Email { get; set; } = String.Empty;
    
    public bool IsAdmin { get; set; } = false;
    
    public bool IsBlocked { get; set; } = false;
    
    public bool IsSubscribedToNewsLetter { get; set; } = false;
}