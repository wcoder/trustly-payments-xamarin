//
// TrustlyJavascriptInterface.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2020 Yauheni Pakala
//

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Webkit;
using Java.Interop;

namespace Xam.Trustly.Droid
{
    public class TrustlyJavascriptInterface : Java.Lang.Object
    {
        public const string NAME = "TrustlyAndroid";

        private readonly Activity _activity;

        public TrustlyJavascriptInterface(Activity activity)
        {
            _activity = activity;
        }

        /// <summary>
        /// Will open the URL, then return result
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="urlScheme"></param>
        /// <returns></returns>
        [Export("openURLScheme")]
        [JavascriptInterface]
        public bool OpenURLScheme(string packageName, string urlScheme)
        {
            if (IsPackageInstalledAndEnabled(packageName, _activity))
            {
                Intent intent = new Intent();
                intent.SetPackage(packageName);
                intent.SetAction(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse(urlScheme));
                _activity.StartActivityForResult(intent, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Helper function that will verify that URL can be opened, then return result
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="context"></param>
        /// <returns>Can Be Opened</returns>
        private bool IsPackageInstalledAndEnabled(string packageName, Context context)
        {
            PackageManager pm = context.PackageManager;
            try
            {
                pm.GetPackageInfo(packageName, PackageInfoFlags.Activities);
                ApplicationInfo ai = pm.GetApplicationInfo(packageName, 0);
                return ai.Enabled;
            }
            catch (PackageManager.NameNotFoundException)
            {
            }
            return false;
        }
    }
}
