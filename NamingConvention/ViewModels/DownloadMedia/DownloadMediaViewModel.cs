using System;
using System.Windows.Input;
using NamingConvention.DependencieServices;
using NamingConvention.EventHandler;
using NamingConvention.Utilities;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.ViewModels.Base;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.DownloadMedia
{

    /// <summary>
    /// Download Manager View Model Calss
    /// </summary>
    
    public class DownloadMediaViewModel : BaseViewModel
    {
        #region Command Declaration
        public ICommand pdfCommand { private set; get; }
        public ICommand imageCommand { private set; get; }
        public ICommand videoCommand { private set; get; }
        public ICommand audioCommand { private set; get; }
        #endregion

        #region Local Veriable
        private IDownloader downloader;
        #endregion

        #region Constuctor declartion
        public DownloadMediaViewModel()
        {
            downloader = DependencyService.Get<IDownloader>();
            pdfCommand = new Command<string>((x) => downloadMediaFile(x));
            imageCommand = new Command<string>((x) => downloadMediaFile(x));
            videoCommand = new Command<string>((x) => downloadMediaFile(x));
            audioCommand = new Command<string>((x) => downloadMediaFile(x));
        }
        #endregion

        #region Button Action

        private void downloadMediaFile(string clickedString)
        {
            if (clickedString == AppTexts.Pdf)
            {
                Uri url = new Uri("http://africau.edu/images/default/sample.pdf");
                string filename = System.IO.Path.GetFileName(url.LocalPath);
                if (downloader.IsFileExist("conventionProducts", filename))
                {
                    Constant.DisplayAlert(AppTexts.AlreadyExist, AppTexts.OkButton, string.Empty);
                }
                else
                {
                    downloader.DownloadFile(url.AbsoluteUri, "conventionProducts", filename);
                }
            }
            if (clickedString == AppTexts.Image)
            {
                Uri url = new Uri("https://www.fnordware.com/superpng/pnggrad16rgb.png");
                string filename = System.IO.Path.GetFileName(url.LocalPath);
                if (downloader.IsFileExist("conventionProducts", filename))
                {
                    Constant.DisplayAlert(AppTexts.AlreadyExist, AppTexts.OkButton, string.Empty);
                }
                else
                {
                    downloader.DownloadFile(url.AbsoluteUri, "conventionProducts", filename);
                }
            }
            if (clickedString == AppTexts.Audio)
            {
                Uri url = new Uri("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3");
                string filename = System.IO.Path.GetFileName(url.LocalPath);
                if (downloader.IsFileExist("conventionProducts", filename))
                {
                    Constant.DisplayAlert(AppTexts.AlreadyExist, AppTexts.OkButton, string.Empty);
                }
                else
                {
                    downloader.DownloadFile(url.AbsoluteUri, "conventionProducts", filename);
                }
            }
            if (clickedString == AppTexts.Video)
            {
                Uri url = new Uri("https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4");
                string filename = System.IO.Path.GetFileName(url.LocalPath);
                if (downloader.IsFileExist("conventionProducts", filename))
                {
                    Constant.DisplayAlert(AppTexts.AlreadyExist, AppTexts.OkButton, string.Empty);
                }
                else
                {
                    downloader.DownloadFile(url.AbsoluteUri, "conventionProducts", filename);
                }
            }
        }
        #endregion

        #region DOWNALOD EVENTHANDLER METHOD
        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e._fileSaved)
            {
                Constant.DisplayAlert("ProjectName", "File Saved Successfully", "Close");
            }
            else
            {
                Constant.DisplayAlert("ProjectName", "Error while saving the file", "Close");
            }
        }
        #endregion

    }
}

