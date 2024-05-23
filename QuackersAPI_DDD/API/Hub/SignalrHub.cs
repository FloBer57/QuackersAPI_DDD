namespace QuackersAPI_DDD.API.Hub
{
    using Microsoft.AspNetCore.SignalR;

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
    }
}
