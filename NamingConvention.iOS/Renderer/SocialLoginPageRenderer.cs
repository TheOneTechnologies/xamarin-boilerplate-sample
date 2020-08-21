using System;
using NamingConvention.iOS.Renderer;
using NamingConvention.Utilities;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.Views.Social;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SocialLogin), typeof(SocialLoginPageRenderer))]
namespace NamingConvention.iOS.Renderer
{
    public class SocialLoginPageRenderer : PageRenderer
    {
        public string accessToken = "";
        bool showLogin = true;



        /// <summary>
        /// When the Facebook or Google button click this method will fire.
        /// </summary>
        /// <param name="animated"></param>
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            try
            {
                //Get and Assign ProviderName from ProviderLoginPage
                var loginPage = Element as SocialLogin;
                string providerName = loginPage.ProviderName;

                //var activity = this.Context as Activity;
                if (showLogin)
                {
                    showLogin = false;

                    //Create OauthProviderSetting class with Oauth Implementation .Refer Step 6
                    OAuthSocialLoginSetting oauth = new OAuthSocialLoginSetting();

                    var auth = oauth.LoginWithProvider(providerName);

                    // After facebook,google and all identity provider login completed 
                    auth.Completed += async (sender, eventArgs) =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            accessToken = eventArgs.Account.Properties["access_token"];

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

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        PresentViewController(auth.GetUI(), true, null);
                    });
                }
            }
            catch (Exception ex)
            {
                Constant.DisplayAlert(AppTexts.AppName, ex.Message, AppTexts.OkButton);
            }
        }
    }
}
