using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using NamingConvention.DependencieServices;
using NamingConvention.Droid.Activities;
using NamingConvention.Droid.DependencieServices;
using NamingConvention.EventHandler;

[assembly: Xamarin.Forms.Dependency(typeof(FileDownloaderAndroid))]

namespace NamingConvention.Droid.DependencieServices
{
    public class FileDownloaderAndroid: IDownloader
    {
        public FileDownloaderAndroid()
        {
        }

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder, string FileName)
        {
            var pathToNewFolder = Path.Combine(MainActivity.mainActivity.GetExternalFilesDir(null).AbsolutePath, folder);
            if (!Directory.Exists(pathToNewFolder))
                Directory.CreateDirectory(pathToNewFolder);
            string filePath = Path.Combine(pathToNewFolder, FileName);
            Directory.CreateDirectory(pathToNewFolder);
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, FileName);
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
        }

        public bool IsFileExist(string folder, string FileName)
        {
            var pathToNewFolder = Path.Combine(MainActivity.mainActivity.GetExternalFilesDir(null).AbsolutePath, folder);
            if (Directory.Exists(pathToNewFolder))
            {
                string filePath = Path.Combine(pathToNewFolder, FileName);
                if (File.Exists(filePath))
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
            else
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
            }
        }
    }
}
