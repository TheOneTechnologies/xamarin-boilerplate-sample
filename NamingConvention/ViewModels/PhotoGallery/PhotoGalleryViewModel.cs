using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NamingConvention.Models.CommonModels;
using NamingConvention.Utilities;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.ViewModels.Base;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.PhotoGallery
{
    public class PhotoGalleryViewModel : BaseViewModel
    {

        #region Constructor
        public PhotoGalleryViewModel()
        {
            ListImages = new ObservableCollection<CarouselModel>();
            CameraCommand = new Command(() => CameraAction());
            GalleryCommand = new Command(() => GalleryAction());
        }
        #endregion

        #region Command Declaration
        public ICommand CameraCommand { private set; get; }
        public ICommand GalleryCommand { private set; get; }
        #endregion
        #region Variable Declaration
        private ObservableCollection<CarouselModel> _listImages;
        public ObservableCollection<CarouselModel> ListImages
        {
            get { return _listImages; }
            set
            {
                _listImages = value;
                OnPropertyChanged("ListImages");
            }
        }
        #endregion

        #region Command Methods

        /// <summary>
        /// Open Device Camera
        /// </summary>
        /// <returns></returns>
        async private void CameraAction()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Constant.DisplayAlert(AppTexts.CameraNotSupported, AppTexts.OkButton, string.Empty);
                return;
            }
            //We need to set the permmision in info.plist for the iOS and for the android we need to set in MainActivity for the Android heigher version.
            //This will check the permmision for camera
            PermissionStatus cameraPermmisionStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            if (cameraPermmisionStatus == PermissionStatus.Granted)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var captureFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                    });
                    if (captureFile == null)
                        return;

                    CarouselModel carouselModel = new CarouselModel();
                    carouselModel.CarouselImage = ImageSource.FromStream(() =>
                    {
                        var stream = captureFile.GetStream();
                        return stream;
                    });
                    ListImages.Add(carouselModel);
                });
            }
            else
                Constant.DisplayAlert(AppTexts.CameraPermmision, AppTexts.OkButton, string.Empty);
        }
        /// <summary>
        /// Open Device Galley To Pick The Image
        /// </summary>
        /// <returns></returns>
        private void GalleryAction()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Constant.DisplayAlert(AppTexts.GalleryNotSupported, AppTexts.OkButton, string.Empty);
                return;
            }
            //We need to set the permmision in info.plist for the iOS and for the android we need to set in MainActivity for the Android heigher version.
            Device.BeginInvokeOnMainThread(async () =>
            {
                var pickFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.MaxWidthHeight,
                });
                if (pickFile == null)
                    return;

                CarouselModel carouselModel = new CarouselModel();
                carouselModel.CarouselImage = ImageSource.FromStream(() =>
                {
                    var stream = pickFile.GetStream();
                    return stream;
                });
                ListImages.Add(carouselModel);

            });
        }
        #endregion
    }
}
