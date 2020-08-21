using System;
using System.Collections.Generic;
using NamingConvention.ViewModels.SideMenu;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NamingConvention.Views.SideMenu
{
    public partial class MenuMasterPage : Xamarin.Forms.MasterDetailPage
    {
        private MasterpageViewModel masterpageViewModel;
        public static MenuMasterPage masterPage;
        public MenuMasterPage()
        {
            InitializeComponent();
            masterPage = this;

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = masterpageViewModel = new MasterpageViewModel();
        }
    }
}
