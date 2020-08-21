using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.ViewModels.Base;
using NamingConvention.Views.DashBoard;
using NamingConvention.Views.DeviceDetails;
using NamingConvention.Views.DownLoadMediaManger;
using NamingConvention.Views.SideMenu;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.SideMenu
{
    public class SideMenuViewModel : BaseViewModel
    {
        #region Local Variable
        public ObservableCollection<SideMenuOptionData> sideMenuOptionDatas { get; set; }
        private SideMenuOptionData _sideMenuoption;
        public SideMenuOptionData SideMenuOption
        {
            get
            {
                return _sideMenuoption;
            }
            set
            {
                _sideMenuoption = value;
                navigateToView(SideMenuOption.Title);
                _sideMenuoption = null;
                OnPropertyChanged("SideMenuOption");
            }
        }
        #endregion
        #region Constructor
        public SideMenuViewModel()
        {
            sideMenuOptionDatas = new ObservableCollection<SideMenuOptionData>();
            addSideMenuOptionData();
        }
        #endregion
        #region CUSTOME METHODS
        public void addSideMenuOptionData()
        {
            sideMenuOptionDatas.Add(new SideMenuOptionData { Title = AppTexts.SideMenuDashBoard, Image = AppImages.SideMenuDeviceDetails });
            sideMenuOptionDatas.Add(new SideMenuOptionData { Title = AppTexts.SideMenuDeviceDetails,Image=AppImages.SideMenuDeviceDetails });
            sideMenuOptionDatas.Add(new SideMenuOptionData { Title = AppTexts.SideMenuDownloadMedia,Image=AppImages.SideMenuSaveMedia });
        }
        public async void navigateToView(string titleName)
        {
            if(titleName == AppTexts.SideMenuDashBoard)
                Application.Current.MainPage = new NavigationPage(new MenuMasterPage());
                MenuMasterPage.masterPage.IsPresented = false;
            if (titleName == AppTexts.SideMenuDeviceDetails)
                await Application.Current.MainPage.Navigation.PushAsync(new DeviceDetailPage());
                MenuMasterPage.masterPage.IsPresented = false;
            if (titleName == AppTexts.SideMenuDownloadMedia)
                await Application.Current.MainPage.Navigation.PushAsync(new DownloadMediaPage());
                MenuMasterPage.masterPage.IsPresented = false;
        }
       
        #endregion
    }
}


