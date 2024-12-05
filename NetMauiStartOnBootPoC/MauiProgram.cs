using Microsoft.Extensions.Logging;
using NetMauiStartOnBootPoC.Platforms.Android;
using NetMauiStartOnBootPoC.Services;

namespace NetMauiStartOnBootPoC
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
                });

            builder.Services.AddMauiBlazorWebView();


#if ANDROID
            builder.Services.AddSingleton<IAppLauncherService, AppLauncherService>();
#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
