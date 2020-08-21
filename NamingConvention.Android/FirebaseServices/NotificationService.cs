using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Firebase.Iid;
using Firebase.Messaging;
using NamingConvention.Droid.Activities;
using NamingConvention.Models.ResponseModels;
using Newtonsoft.Json;

namespace NamingConvention.Droid.FirebaseServices
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class NotificationService : FirebaseMessagingService
    {



        public override void OnNewToken(string p0)
        {
            string firebaseToken = p0;
            base.OnNewToken(p0);

        }

        /// <summary>
        /// Receive the firebase push notification here
        /// </summary>
        /// <param name="message"></param>
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                SendNotification(message.GetNotification().Body);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        #region Create Local Notificaiton.
        /// <summary>
        /// Send the local notification
        /// </summary>
        /// <param name="body"></param>
        void SendNotification(string body)
        {
            try
            {
                NotificationManager notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
                Intent homeIntent = new Intent(Application.Context, typeof(MainActivity));
                var pendingIntent = PendingIntent.GetActivity(ApplicationContext, (int)Java.Lang.JavaSystem.CurrentTimeMillis(), homeIntent, PendingIntentFlags.UpdateCurrent);
                Bitmap largeIcon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_logo);
                NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, "my_channel_01");
                var notification = builder.SetContentIntent(PendingIntent.GetActivity(Application.Context, 0, homeIntent, 0)).SetSmallIcon(Resource.Drawable.ic_logo)
                .SetContentText(body).SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification)).SetAutoCancel(true).Build();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    //Create the chennel
                    NotificationChannel channel = new NotificationChannel("my_channel_01", "GlobalDemo", NotificationImportance.High);
                    channel.SetShowBadge(false);
                    channel.LockscreenVisibility = NotificationVisibility.Public;
                    notificationManager.CreateNotificationChannel(channel);
                }
                notificationManager?.Notify(1, notification);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        #endregion
    }
}

