namespace ServerBlazor.Models
{
    public class MySharedService : IMySharedService
    {
        public event Action OnRefreshRequested;

        public void RequestRefresh()
        {
            OnRefreshRequested?.Invoke();
        }
    }
}
