using Microsoft.JSInterop;

namespace BookClient.Services.Notification;

public interface INotificationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public Task NotifyAsync(string message, int duration = 3000);
}