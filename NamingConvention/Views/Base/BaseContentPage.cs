using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NamingConvention.Views.Base
{
    /// <summary>
    /// Base Content Page Class 
    /// </summary>
    public class BaseContentPage : ContentPage
    {
        #region Constructor
        public BaseContentPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        #endregion
    }
}

