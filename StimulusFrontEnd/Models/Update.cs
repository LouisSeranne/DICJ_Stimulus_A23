namespace StimulusFrontEnd.Models
{
    public interface IUpdateService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }

    public class UpdateService : IUpdateService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
