using Microsoft.JSInterop;

namespace BookClient.Services.Notification;

public sealed class NotificationService : INotificationService
{
    private readonly IJSRuntime _jsRuntime;

    public NotificationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    async Task INotificationService.NotifyAsync(string message, int duration = 3000)
    {
        await _jsRuntime.InvokeVoidAsync("toastifyWrapper.showToast", message, duration);
    }
}