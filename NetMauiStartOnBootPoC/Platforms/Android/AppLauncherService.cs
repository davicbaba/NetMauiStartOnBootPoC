using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using NetMauiStartOnBootPoC.Services;
using AndroidProvider = Android.Provider;
using AndroidNet = Android.Net;

namespace NetMauiStartOnBootPoC.Platforms.Android
{
    internal class AppLauncherService : IAppLauncherService
    {
        public void AskManageOverlayPermission()
        {
            var context = Platform.AppContext;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q && !AndroidProvider.Settings.CanDrawOverlays(context))
            {
                var intent = new Intent(AndroidProvider.Settings.ActionManageOverlayPermission,
                    AndroidNet.Uri.Parse($"package:{context.PackageName}"));

                intent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(intent);
            }
        }

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
