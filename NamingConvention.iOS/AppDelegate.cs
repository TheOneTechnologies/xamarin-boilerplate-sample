using System.Collections.Generic;
using Foundation;
using Plugin.FirebasePushNotification;
using UIKit;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace NamingConvention.iOS
{
    /// <summary>
    /// Entry point of Xamarin.iOS Application
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        ///  Setup Xamarin.Forms project for iOS in this method(Package initialize)
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            //Set flag to use this controls
            Xamarin.Forms.Forms.SetFlags(new string[] { "CarouselView_Experimental", "IndicatorView_Experimental" });
            global::Xamarin.Forms.Forms.Init();

            ///Uncomment this line of code when you add your google firebase json file
            ////Initialize the firebase push notification
            //FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
            //{
            //    new NotificationUserCategory("message",new List<NotificationUserAction> { new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground) }),
            //    new NotificationUserCategory("request",new List<NotificationUserAction> { new NotificationUserAction("Accept","Accept"), new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)})
            //});


            //Initialize the Xamarin Forms Map
            Xamarin.FormsMaps.Init();
            //App center intialize
            AppCenter.Start("2fa4b2c7-5230-489e-8045-dec85a561855",typeof(Analytics), typeof(Crashes));
            Firebase.Core.App.Configure();
            LoadApplication(new App.App());
            string userAgent = "Mozilla / 5.0(Macintosh; Intel Mac OS X 10_10_5) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 45.0.2454.99 Safari / 537.36";
            NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);
            return base.FinishedLaunching(app, options);
        }
    }
}
