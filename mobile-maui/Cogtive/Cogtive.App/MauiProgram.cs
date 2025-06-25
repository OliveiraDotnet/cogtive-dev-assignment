using Cogtive.App.Data;
using Cogtive.App.Data.Repository;
using Cogtive.App.Interfaces;
using Cogtive.App.Services;
using Cogtive.App.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cogtive.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var dbName = "cogtive.db";
                var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var dbPath = Path.Combine(dbFolder, dbName);
                options.UseSqlite($"Filename={dbPath}");
            });

            //Http 
            builder.Services.AddSingleton<HttpClient>();
            
            //Services
            builder.Services.AddSingleton<IAppStateService, AppStateService>();
            builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddTransient<ISyncService,SyncService>();
            builder.Services.AddTransient<ILocalDataService,LocalDataService>();

            //Repositories
            builder.Services.AddScoped<ProductionDataRepository>();

            //Pages
            builder.Services.AddTransient<MainPage>();
            
            //ViewModels
            builder.Services.AddTransient<MainPageVM>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
