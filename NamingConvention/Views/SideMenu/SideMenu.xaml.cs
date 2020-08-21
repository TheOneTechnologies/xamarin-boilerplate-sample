using System;
using System.Collections.Generic;
using NamingConvention.ViewModels.SideMenu;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NamingConvention.Views.SideMenu
{
    public partial class SideMenu : ContentPage
    {
        private SideMenuViewModel sideMenuViewModel;
        public SideMenu()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = sideMenuViewModel = new SideMenuViewModel();
        }
    }
}
