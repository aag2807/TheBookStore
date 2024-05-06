using Core.User;

namespace BookClient.Models.Messaging;

public sealed class Message
{
    public User User { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    private Message(User user, string content)
    {
        User = user;
        Content = content;
    }

    public static Message FromNoLoggedInUser(string content)
    {
        User notLoggedInUser = new()
        {
            Username = "Anonymous",
            Email = "No Email",
            UserId = -1,
        };

        return new Message(notLoggedInUser, content);
    }

    public static Message FromLoggedInUser(User user, string content)
    {
        return new Message(user, content);
    }
}