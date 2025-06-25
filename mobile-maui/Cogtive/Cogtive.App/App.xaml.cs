using Cogtive.App.Interfaces;

namespace Cogtive.App
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = new AppShell();

            var connectivityService = serviceProvider.GetRequiredService<IConnectivityService>();
            connectivityService.StartMonitoring();
        }
    }
}
