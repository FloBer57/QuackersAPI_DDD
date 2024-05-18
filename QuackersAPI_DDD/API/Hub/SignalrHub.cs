namespace QuackersAPI_DDD.API.Hub
{
    using Microsoft.AspNetCore.SignalR;

    public class SignalrHub : Hub
    {
        public async Task NewMessage(int userId, string message)
        {
            await Clients.All.SendAsync("messageReceived", userId, message);
        }
    }
}
