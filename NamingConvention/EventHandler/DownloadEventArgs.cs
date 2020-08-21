using System;
namespace NamingConvention.EventHandler
{
    /// <summary>
    /// 
    /// </summary>
    public class DownloadEventArgs : EventArgs
    {
        //Local vaiable 
        public bool _fileSaved;

        // DownloadEventArgs Constructor
        public DownloadEventArgs(bool fileSaved)
        {
            _fileSaved = fileSaved;
        }
    }
}
