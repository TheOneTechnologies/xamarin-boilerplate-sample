using System;
using System.Threading.Tasks;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Utilities;
using NamingConvention.Views.DashBoard;
using NamingConvention.Views.Login;
using NamingConvention.Views.SideMenu;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NamingConvention.App
{
    public partial class App : Application
    {
        #region Constructor
        public App()
        {
            InitializeComponent();
            //Check Internet Connection
            Task.Run(async () => await CheckInternetConnection()).Wait();
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                try
                {
                    if (args.IsConnected)
                        Constant.IsInternet = true;
                    else
                        Constant.IsInternet = false;
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    Constant.IsInternet = false;
                }
            };
            CheckLoginStatus();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check Internet Connection
        /// </summary>
        /// <returns></returns>
        private async Task CheckInternetConnection()
        {
            try
            {
                Constant.IsInternet = await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com/");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Constant.IsInternet = false;
            }
        }
        /// <summary>
		///  Handle when your app starts
		/// </summary>
        protected override void OnStart()
        {
            #region Firebase Push notification methods

            ///Uncomment this line of code when you add your google firebase json file

            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    string firebaseToken = p.Token;
            //};
            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //};
            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //};
            //CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            //{
            //};
            //CrossFirebasePushNotification.Current.OnNotificationError += (s, p) =>
            //{
            //};
            #endregion
        }

        /// <summary>
		/// Handle when your app sleeps
		/// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
		///  Handle when your app resumes
		/// </summary>
        protected override void OnResume()
        {
        }

        /// <summary>
		/// Check Current Login Status
		/// </summary>
        public static void CheckLoginStatus()
        {
            Current.MainPage = new Login();

        }
        #endregion
    }
}