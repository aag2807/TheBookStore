namespace Core.User;

public sealed class User
{
    public int UserId { get; set; }
    
    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;
    
    public string Email { get; set; } = String.Empty;
    
    public bool IsAdmin { get; set; } = false;
    
    public bool IsBlocked { get; set; } = false;
}