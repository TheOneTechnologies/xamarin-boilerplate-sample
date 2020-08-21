using System;
using System.Collections.Generic;
using NamingConvention.DependencieServices;
using NamingConvention.ViewModels.DownloadMedia;
using Xamarin.Forms;

namespace NamingConvention.Views.DownLoadMediaManger
{
    public partial class DownloadMediaPage : ContentPage
    {
        #region Local Variable
        private DownloadMediaViewModel downloadMediaViewModel;
        #endregion
        public DownloadMediaPage()
        {
            InitializeComponent();
            BindingContext = downloadMediaViewModel = new DownloadMediaViewModel();
            
        }
    }
}
