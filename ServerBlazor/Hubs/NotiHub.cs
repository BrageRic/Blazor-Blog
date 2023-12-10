using Microsoft.AspNetCore.SignalR;

namespace ServerBlazor.Hubs
{
    public class NotiHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
