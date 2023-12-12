namespace ServerBlazor.Models
{
    public interface IMySharedService
    {
        event Action OnRefreshRequested;
        void RequestRefresh();
    }
}
