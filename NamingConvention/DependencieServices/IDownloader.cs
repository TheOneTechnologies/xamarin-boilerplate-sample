using System;
using NamingConvention.EventHandler;

namespace NamingConvention.DependencieServices
{
    /// <summary>
    /// Interface for download media file
    /// </summary>
    public interface IDownloader
    {
        void DownloadFile(string url, string folder, string FileName);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        bool IsFileExist(string folder, string FileName);
    }
}
