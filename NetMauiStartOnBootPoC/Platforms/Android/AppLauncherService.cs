using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
using NetMauiStartOnBootPoC.Services;

namespace NetMauiStartOnBootPoC.Platforms.Android
{
    internal class AppLauncherService : IAppLauncherService
    {
        public void RestartApplication()
        {
            var context = Platform.AppContext;
            PackageManager packageManager = context.PackageManager!;
            Intent intent = packageManager!.GetLaunchIntentForPackage(context.PackageName!)!;
            ComponentName componentName = intent.Component!;
            Intent mainIntent = Intent.MakeRestartActivityTask(componentName)!;
            mainIntent.SetPackage(context.PackageName);
            context.StartActivity(mainIntent);
            Runtime.GetRuntime()!.Exit(0);
        }
    }
}
