using Microsoft.AspNetCore.SignalR;
using ServerBlazor.Models.Entities;

namespace ServerBlazor.Hubs
{
    public class NotiHub : Hub<INotiClient>
    {
        static int clientsCount;

        public override async Task OnConnectedAsync()
        {
            clientsCount++;
            await Clients.All.UpdateClientsCount(clientsCount);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientsCount--;
            await Clients.All.UpdateClientsCount(clientsCount);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ReleasePost()
        {
            await Clients.Others.ReceivePost();
        }

        public async Task ReleaseComment()
        {
            await Clients.Others.ReceiveComment();
        }

    }
}
