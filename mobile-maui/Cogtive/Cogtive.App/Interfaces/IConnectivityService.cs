namespace Cogtive.App.Interfaces
{
    public interface IConnectivityService
    {
        bool IsConnected { get; }
        event Action ConnectivityChanged;
        void StartMonitoring();
        void StopMonitoring();
    }
}
