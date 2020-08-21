using System;
using System.Collections.Generic;
using NamingConvention.ViewModels.DeviceDetails;
using Xamarin.Forms;

namespace NamingConvention.Views.DeviceDetails
{
    public partial class DeviceDetailPage : ContentPage
    {
        #region Local Variable
        private DeviceDetailViewModel DeviceDetailViewModel;
        #endregion
        public DeviceDetailPage()
        {
            InitializeComponent();
            BindingContext = DeviceDetailViewModel = new DeviceDetailViewModel();
        }
    }
}
