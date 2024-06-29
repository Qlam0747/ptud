using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Sharing_Place
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Times.New.Roman_n.ttf", "TimesNewRomanRegular");
                    fonts.AddFont("timesbd.ttf", "TimesNewRomanbold");
                });
            //var ConnectString = builder.Configuration["ConnectionStrings:SqlConnectString"];

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
