using Simbad.State.Attributes;

namespace BookClient.Services.State.Authentication;

[State(typeof(Authentication), "Authentication")]
public sealed class Authentication
{
    public bool IsUserLoggedIn { get; private set; } = false;

    public Core.User.User? LoggedInUser { get; private set; } = null;

    public bool UserHasSubscription => LoggedInUser?.IsSubscribedToNewsLetter ?? false;

    public async Task LoginUserAsync()
    {
        
    }
}