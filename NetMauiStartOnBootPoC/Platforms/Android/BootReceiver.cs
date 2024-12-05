using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.ContactsContract.CommonDataKinds;
using static Java.Util.Jar.Attributes;
using AndroidContent = Android.Content;
using AndroidUtil = Android.Util;
using AndroidProvider = Android.Provider;
using AndroidNet = Android.Net;


namespace NetMauiStartOnBootPoC.Platforms.Android
{
    [BroadcastReceiver(Name = "com.companyname.netmauistartonbootpoc.BootReceiver", Exported = true, Enabled = true, DirectBootAware = true)]
    [IntentFilter(new[] {  Intent.ActionBootCompleted}, Categories = new[] { Intent.CategoryDefault })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == null || (!intent.Action.Equals(Intent.ActionBootCompleted))) 
                return;

            if (intent.Action == Intent.ActionBootCompleted)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    if (!AndroidProvider.Settings.CanDrawOverlays(context))
                    {
                        var overlayIntent = new Intent(AndroidProvider.Settings.ActionManageOverlayPermission,
                            AndroidNet.Uri.Parse("package:" + context.PackageName));
                        overlayIntent.AddFlags(ActivityFlags.NewTask);
                        context.StartActivity(overlayIntent);
                        return;
                    }
                }

                // Lanza la MainActivity
                var activityIntent = new Intent(context, typeof(MainActivity));
                activityIntent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(activityIntent);
            }
        }
    }
}
