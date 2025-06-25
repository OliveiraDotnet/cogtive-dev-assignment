using Cogtive.App.Interfaces;

namespace Cogtive.App.Services
{
    internal class ConnectivityService : IConnectivityService
    {
        public event Action ConnectivityChanged;

        private readonly ISyncService _syncService;
        private readonly IAppStateService _appState;

        public ConnectivityService(ISyncService syncService, IAppStateService appState)
        {
            _syncService = syncService;
            _appState = appState;
        }

        public bool IsConnected => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

        public void StartMonitoring()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;

            UpdateState(Connectivity.Current.NetworkAccess);
            if (_appState.IsOnline)
                _ = TrySyncAsync();
        }

        public void StopMonitoring()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            UpdateState(e.NetworkAccess);

            if (_appState.IsOnline)
                _ = TrySyncAsync();
        }

        private void UpdateState(NetworkAccess access)
        {
            bool wasOnline = _appState.IsOnline;
            _appState.IsOnline = access == NetworkAccess.Internet;

            if (_appState.IsOnline != wasOnline)
                ConnectivityChanged?.Invoke();
        }

        private async Task TrySyncAsync()
        {
            try
            {
                await _syncService.SyncPendingOperationsAsync();
            }
            catch (Exception ex)
            {
                // Log
            }
        }
    }
}
