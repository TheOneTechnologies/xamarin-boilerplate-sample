using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Android;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Plugin.FirebasePushNotification;
using Firebase.Iid;
using Plugin.Permissions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace NamingConvention.Droid.Activities
{
    /// <summary>
    /// Entry Point of xamarin.Android Application
    /// </summary>
    [Activity(Label = "NamingConvention", Icon = "@mipmap/icon", Theme = "@style/MyTheme.Splash",
              MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region Methods
        /// <summary>
        /// Setup Xamarin.Forms project for android in this method (Package initialize)
        /// </summary>
        /// <param name="savedInstanceState"></param>
        ///

        public static MainActivity mainActivity;
        static string[] PERMISSIONS_Storage = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);

            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);

            //Ask for the Permission
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessMockLocation) != Permission.Granted)

            {
                ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.Camera,Manifest.Permission.AccessFineLocation,Manifest.Permission.AccessCoarseLocation,
                    Manifest.Permission.AccessMockLocation,Manifest.Permission.ReadExternalStorage,Manifest.Permission.WriteExternalStorage
                }, 1);
            }
            //Initialize the Xamarin Forms Map
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            //App center intialize
            AppCenter.Start("a58bbf55-78f8-4fc7-ba7f-3e695f838f18",typeof(Analytics), typeof(Crashes));

            //Create notification chennel for Android 8.0 or heigher version
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";
                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            mainActivity = this;

            LoadApplication(new App.App());
        }
        /// <summary>
        /// Get permission results
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="permissions"></param>
        /// <param name="grantResults"></param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {  
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion
    }
}
