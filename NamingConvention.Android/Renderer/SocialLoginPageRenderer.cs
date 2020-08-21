using System;
using Android.App;
using Android.Content;
using NamingConvention.Droid.Renderer;
using NamingConvention.Utilities;
using NamingConvention.Views.Social;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SocialLogin), typeof(SocialLoginPageRenderer))]
namespace NamingConvention.Droid.Renderer
{
    public class SocialLoginPageRenderer : PageRenderer
    {
        bool showLogin = true;
        public SocialLoginPageRenderer(Context context) : base(context)
        {
        }


        /// <summary>
        /// When the Facebook or Google button click this method will fire.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            //Get and Assign ProviderName from ProviderLoginPage
            var loginPage = Element as SocialLogin;
            string providerName = loginPage.ProviderName;

            var activity = this.Context as Activity;
            if (showLogin)
            {
                showLogin = false;

                //Create OauthProviderSetting class with Oauth Implementation.
                OAuthSocialLoginSetting oauth = new OAuthSocialLoginSetting();

                var auth = oauth.LoginWithProvider(providerName);

                // After facebook,google and all identity provider login completed 
                auth.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                    {

                        // Get and Save User Details 
                        var accessToken = eventArgs.Account.Properties["access_token"];
                        //GetGoogleUserProfileAsync(App.AccessToken);

                        if (providerName == "Facebook")
                        {
                            await Constant.GetFacebookProfile(accessToken);
                        }
                        else
                        {
                            await Constant.GetGoogleProfile(accessToken);
                        }
                    }
                    else
                    {
                        // The user cancelled
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (App.App.Current.MainPage.Navigation.ModalStack.Count > 0)
                                App.App.Current.MainPage.Navigation.PopModalAsync();
                        });

                    }
                };


                //Get login error
                auth.Error += (object sender, AuthenticatorErrorEventArgs ed) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (App.App.Current.MainPage.Navigation.ModalStack.Count > 0)
                            App.App.Current.MainPage.Navigation.PopModalAsync();
                    });
                };
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }
}