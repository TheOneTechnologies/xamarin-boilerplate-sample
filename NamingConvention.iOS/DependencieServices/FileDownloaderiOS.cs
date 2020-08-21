using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using NamingConvention.DependencieServices;
using NamingConvention.EventHandler;
using NamingConvention.iOS.DependencieServices;

[assembly: Xamarin.Forms.Dependency(typeof(FileDownloaderiOS))]

namespace NamingConvention.iOS.DependencieServices
{
    public class FileDownloaderiOS: IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        
        public void DownloadFile(string url, string folder, string FileName)
        {
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);
            Directory.CreateDirectory(pathToNewFolder);
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
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
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);
            string pathToNewFile = Path.Combine(pathToNewFolder, FileName);
            if (File.Exists(pathToNewFile))
            {
                return true;
            }
            else
            {
                return false;
            }
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
