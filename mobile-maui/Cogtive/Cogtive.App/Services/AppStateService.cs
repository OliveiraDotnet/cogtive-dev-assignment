using Cogtive.App.Interfaces;
using System.ComponentModel;

namespace Cogtive.App.Services
{
    public class AppStateService : IAppStateService, INotifyPropertyChanged
    {
        private bool _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    OnPropertyChanged(nameof(IsOnline));
                    OnPropertyChanged(nameof(IsOffline));
                }
            }
        }

        public bool IsOffline => !IsOnline;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
