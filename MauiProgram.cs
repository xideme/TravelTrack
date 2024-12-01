using Microsoft.Extensions.Logging;
using TravelTrack.Services;

namespace TravelTrack
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "travelplanner.db");
            builder.Services.AddSingleton(s => new DatabaseService(dbPath));


            return builder.Build();
        }
    }
}
