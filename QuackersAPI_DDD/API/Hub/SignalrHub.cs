namespace QuackersAPI_DDD.API.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using QuackersAPI_DDD.Domain.Model;

    public class SignalrHub : Hub
    {
        public async Task NewMessage(int userId, string message, int channelId)
        {
            await Clients.Group(channelId.ToString()).SendAsync("messageReceived", userId, message);
        }

        public async Task JoinChannel(int channelId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
        }

        public async Task ExitChannel(int channelId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
        }

        public async Task SendNotification(int notificationId, string notificationName, string notificationText, int notificationType)
        {

            await Clients.AllExcept(notificationId.ToString()).SendAsync("ReceiveNotification", new
            {
                NotificationId = notificationId,
                NotificationName = notificationName,
                NotificationText = notificationText,
                NotificationType = notificationType
            });

        }
    }

}

