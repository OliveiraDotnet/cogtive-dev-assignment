using Cogtive.App.Interfaces;
using Cogtive.App.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Cogtive.App.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IApiService _apiService;
        private readonly IConnectivityService _connectivityService;
        private readonly ILocalDataService _localDataService;

        public ICommand SyncCommand { get; }
        public ICommand SubmitDataCommand { get; }
        public ICommand ScanQRCodeCommand { get; }

        public MainPageVM(IApiService apiService, IConnectivityService connectivityService, ILocalDataService localDataService)
        {
            _apiService = apiService;
            _connectivityService = connectivityService;
            _localDataService = localDataService;

            _connectivityService.ConnectivityChanged += OnConnectivityChanged;

            SyncCommand = new Command(async () => await SyncData());
            ScanQRCodeCommand = new Command(async () => await ScanQRCode());
            SubmitDataCommand = new Command(async () => await SubmitProductionData(), CanSubmit);

            _ = LoadMachines();
        }

        public ObservableCollection<Machine> Machines { get; private set; } = [];
        public Machine SelectedMachine { get; set; }
        public string ConnectionStatusMessage { get; set; } = "Checking...";
        public Color ConnectionStatusColor { get; set; } = Colors.Gray;
        public bool IsMachineSelected => SelectedMachine != null;
        public string SelectedMachineStatus => SelectedMachine?.IsActive == true ? "Active" : "Inactive";
        public bool IsOnline => _connectivityService.IsConnected;
        public bool CanSync => PendingUploads > 0 && IsOnline;
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
        public decimal? Efficiency { get; set; }
        public int? UnitsProduced { get; set; }
        public int? Downtime { get; set; }
        public int PendingUploads { get; set; }
        public bool IsLoading { get; set; }
        public string ErrorMessage { get; set; }

        private void OnConnectivityChanged()
        {
            RaisePropertyChanged(nameof(IsOnline));
            RaisePropertyChanged(nameof(CanSync));

            UpdateConnectionStatusMessage();

            _ = LoadMachines();
        }

        private async Task LoadMachines()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                Machines.Clear();
                List<Machine> machines;

                if (IsOnline)
                {
                    machines = await _apiService.GetMachinesAsync();
                    // cache local
                    await _localDataService.SaveMachinesAsync(machines); 
                }
                else
                {
                    machines = await _localDataService.GetMachinesAsync();
                }

                foreach (var machine in machines)
                    Machines.Add(machine);

                await UpdateSyncStatus();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading machines: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }

            UpdateConnectionStatusMessage();
        }

        private async Task UpdateSyncStatus()
        {
            var unsynced = await _localDataService.GetUnsyncedProductionDataAsync();
            PendingUploads = unsynced.Count;
            RaisePropertyChanged(nameof(CanSync));
        }

        private async Task SyncData()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var unsynced = await _localDataService.GetUnsyncedProductionDataAsync();

                foreach (var data in unsynced)
                {
                    await _apiService.PostProductionDataAsync(data);
                    await _localDataService.MarkProductionDataAsSyncedAsync(data.Id);
                }

                PendingUploads = 0;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Sync failed: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }

            RaisePropertyChanged(nameof(CanSync));
        }

        private async Task SubmitProductionData()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                if (SelectedMachine == null)
                {
                    ErrorMessage = "No machine selected.";
                    return;
                }

                var data = new ProductionData
                {
                    MachineId = SelectedMachine.Id,
                    Efficiency = Efficiency.Value,
                    UnitsProduced = UnitsProduced.Value,
                    Downtime = Downtime.Value,
                    Timestamp = DateTime.UtcNow,
                    IsSynced = false
                };

                await _localDataService.SaveProductionDataAsync(data);

                Efficiency = null;
                UnitsProduced = null;
                Downtime = null;

                await UpdateSyncStatus();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to save data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool CanSubmit() =>
            Efficiency.HasValue &&
            UnitsProduced.HasValue &&
            Downtime.HasValue &&
            SelectedMachine != null;

        private void UpdateConnectionStatusMessage()
        {
            var profiles = Connectivity.Current.ConnectionProfiles;
            var access = Connectivity.Current.NetworkAccess;

            if (access != NetworkAccess.Internet)
            {
                ConnectionStatusMessage = "No Internet";
                ConnectionStatusColor = Colors.Red;
            }
            else if (profiles.Contains(ConnectionProfile.WiFi))
            {
                ConnectionStatusMessage = "Connected (WiFi)";
                ConnectionStatusColor = Colors.Green;
            }
            else if (profiles.Contains(ConnectionProfile.Cellular))
            {
                ConnectionStatusMessage = "Connected (Mobile)";
                ConnectionStatusColor = Colors.Green;
            }
            else
            {
                ConnectionStatusMessage = "Connected";
                ConnectionStatusColor = Colors.Green;
            }

            RaisePropertyChanged(nameof(ConnectionStatusMessage));
            RaisePropertyChanged(nameof(ConnectionStatusColor));
        }

        private async Task ScanQRCode()
        {
            await Task.Delay(1000);
            ErrorMessage = "QR Code scan not implemented yet.";
        }

        private void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
