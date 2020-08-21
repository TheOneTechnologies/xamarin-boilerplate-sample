using System;
using System.Collections.ObjectModel;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.DeviceDetails
{
    /// <summary>
    /// Device Detail View Model Calss
    /// </summary>
    public class DeviceDetailViewModel : BaseViewModel
    {

        #region Local Variable
        public ObservableCollection<DeviceDetailData> deviceDetails { get; set; }
        #endregion

        #region Variable Declaration
        private string buildNumber;
        public string BuildNumber
        {
            get { return buildNumber; }
            set
            {
                buildNumber = value;
                OnPropertyChanged("BuildNumber");
            }
        }
        private string versionNumber;
        public string VersionNumber
        {
            get { return versionNumber; }
            set
            {
                versionNumber = value;
                OnPropertyChanged("VersionNumber");
            }
        }
        private DeviceDetailData selectedDetail;
        public DeviceDetailData SelectedDetail
        {
            get { return selectedDetail; }
            set
            {
                selectedDetail = value;
                selectedDetail = null;
                OnPropertyChanged("SelectedDetail");
            }
        }
        #endregion
        public DeviceDetailViewModel()
        {
            deviceDetails = new ObservableCollection<DeviceDetailData>();
            addDeviceDetails();
        }
        #region CUSTOME METHODS
        public void addDeviceDetails()
        {
            VersionNumber = "Application Version " + AppInfo.VersionString;
            BuildNumber = "Application Build Number " + AppInfo.BuildString;

            deviceDetails.Add(new DeviceDetailData { InformationTitle = AppTexts.DeviceName, InformationData = DeviceInfo.Name });
            deviceDetails.Add(new DeviceDetailData { InformationTitle = AppTexts.DeviceType, InformationData = DeviceInfo.DeviceType.ToString() });
            deviceDetails.Add(new DeviceDetailData { InformationTitle = AppTexts.DevicePlatform, InformationData = DeviceInfo.Platform.ToString() });

            deviceDetails.Add(new DeviceDetailData { InformationTitle = AppTexts.DeviceOSVersion, InformationData = DeviceInfo.Version.ToString() });
            deviceDetails.Add(new DeviceDetailData { InformationTitle = AppTexts.DeviceModelName, InformationData = DeviceInfo.Model.ToString() });
        }
        
        #endregion

    }
}

