using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Service;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.Views.DashBoard;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NamingConvention.Utilities
{
    /// <summary>
    /// Static Constant class
    /// </summary>
    public static class Constant
    { 
        #region Static Variable
        /// <summary>
        /// Login Response model
        /// </summary>
        public static LoginResponse LoginResponse { get; set; }
        public static bool IsInternet { get; set; }
        #endregion

        #region Loader And Alert

        /// <summary>
        /// Show Loader
        /// </summary>
        /// <param name="loadingText"></param>
        public static void ShowLoader(string loadingText)
        {
            UserDialogs.Instance.ShowLoading(loadingText);
        }

        /// <summary>
        /// Hide Loader
        /// </summary>
        public static void HideLoader()
        {
            UserDialogs.Instance.HideLoading();
        }

        /// <summary>
        /// Display Alert
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="okButtonTitle"></param>
        /// <param name="cancelButtonTitle"></param>
        public static void DisplayAlert(string message, string okButtonTitle, string cancelButtonTitle)
        {
            if (string.IsNullOrWhiteSpace(cancelButtonTitle))
                Application.Current.MainPage.DisplayAlert(AppTexts.AppName, message, okButtonTitle);
            else
                Application.Current.MainPage.DisplayAlert(AppTexts.AppName, message, okButtonTitle, cancelButtonTitle);
        }
        #endregion

        #region Save and Get Local Prefrence

        /// <summary>
        /// Save Prefrence 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task SavePrefrence<T>(string Key, T value)
        {
            Application.Current.Properties[Key] = value;
            await Application.Current.SavePropertiesAsync();
        }

        /// <summary>
        /// Get Prefrence
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static object GetPrefrence(string Key)
        {
            if (Application.Current.Properties.ContainsKey(Key))
                return Application.Current.Properties[Key];
            else
                return null;
        }
        #endregion

        #region Validation methods

        /// <summary>
        /// Validate Email
        /// </summary>
        /// <param name="emailString"></param>
        /// <returns></returns>
        public static bool ValidateEmail(string emailString)
        {
            string email = emailString;
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return (Regex.IsMatch(emailString, regex));
        }

        /// <summary>
        /// Validate Passowrd
        /// </summary>
        /// <param name="passwordString"></param>
        /// <returns></returns>
        public static bool ValidatePassowrd(string passwordString)
        {
            string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";
            return (Regex.IsMatch(passwordString, passwordRegex));
        }

        /// <summary>
        /// Validate PhonNumber
        /// </summary>
        /// <param name="phoneNumberString"></param>
        /// <returns></returns>
        public static bool ValidatePhonNumber(string phoneNumberString)
        {
            return Regex.Match(phoneNumberString, @"^(\+[0-9]{9})$").Success;
        }

        #endregion

        /// <summary>
        /// Get User's Current Location
        /// </summary>
        async public static Task<Xamarin.Essentials.Location> GetUserCurrentLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var userLocation = await Geolocation.GetLastKnownLocationAsync();
            return await Geolocation.GetLocationAsync(request);
        }
        #region Get Profile from Social Login

        /// <summary>
        /// Get FaceBook Profile From API Call
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        async public static Task GetFacebookProfile(string accessToken)
        {
            try
            {
                if (Constant.IsInternet)
                {
                    if (App.App.Current.MainPage.Navigation.ModalStack.Count > 0)
                        await App.App.Current.MainPage.Navigation.PopModalAsync();

                    await Task.Run(() =>
                    {
                        Constant.ShowLoader(AppTexts.Loading);
                    });
                    var results = await APIService.Get(APIServicePath.FacebookAPIRequest + accessToken);

                    await Task.Run(() =>
                    {
                        Constant.HideLoader();
                    });
                    if (results.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrWhiteSpace(results.Content))
                    {
                        LoginResponse loginResponse = new LoginResponse();
                        loginResponse.Access_Token = accessToken;
                        loginResponse.Expires_In = 10;
                        Constant.LoginResponse = loginResponse;

                        App.App.CheckLoginStatus();
                    }
                }
                else
                    Constant.DisplayAlert(AppTexts.NoInternet, AppTexts.OkButton, string.Empty);
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                await Task.Run(() =>
                {
                    Constant.HideLoader();
                });
            }
        }
        /// <summary>
        /// Get Google Profile From API Call
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        async public static Task GetGoogleProfile(string accessToken)
        {
            try
            {
                if (Constant.IsInternet)
                {
                    if (App.App.Current.MainPage.Navigation.ModalStack.Count > 0)
                        await App.App.Current.MainPage.Navigation.PopModalAsync();

                    await Task.Run(() =>
                    {
                        Constant.ShowLoader(AppTexts.Loading);
                    });
                    var results = await APIService.Get(APIServicePath.GoogleAPIRequest + accessToken);

                    await Task.Run(() =>
                    {
                        Constant.HideLoader();
                    });
                    if (results.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrWhiteSpace(results.Content))
                    {

                        var googleProfileResponse = JsonConvert.DeserializeObject<GoogleProfileResponse>(results.Content);
                        if (googleProfileResponse != null)
                        {
                            LoginResponse loginResponse = new LoginResponse();
                            loginResponse.Access_Token = accessToken;
                            loginResponse.Expires_In = 10;
                            Constant.LoginResponse = loginResponse;
                            App.App.CheckLoginStatus();
                        }
                    }
                }
                else
                    Constant.DisplayAlert(AppTexts.NoInternet, AppTexts.OkButton, string.Empty);
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                await Task.Run(() =>
                {
                    Constant.HideLoader();
                });
            }
        }

        #endregion
    }
}
