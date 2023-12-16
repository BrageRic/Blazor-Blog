using ServerBlazor.Models.Entities;

namespace ServerBlazor.Hubs
{
    public interface INotiClient
    {
        Task ReceivePost();
        Task ReceiveComment();
        Task UpdateClientsCount(int count);
    }
}
