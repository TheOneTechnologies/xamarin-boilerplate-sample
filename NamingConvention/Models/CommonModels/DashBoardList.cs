using System.Collections.Generic;
using NamingConvention.ViewModels.Base;

namespace NamingConvention.Models.ResponseModels
{
    /// <summary>
    /// Home Menu Model
    /// </summary>
    public class DashBoardItems : BaseViewModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public double Radius { get; set; }
        public double Height { get; set; }

        private bool _unreadCountVisible;
        public bool UnreadCountVisible
        {
            get { return _unreadCountVisible; }
            set
            {
                _unreadCountVisible = value;
                OnPropertyChanged("UnreadCountVisible");
            }
        }
        private int _unReadCount;
        public int UnReadCount
        {
            get { return _unReadCount; }
            set
            {
                _unReadCount = value;
                OnPropertyChanged("UnReadCount");
            }
        }
    }

    /// <summary>
    /// DashBoard Data Model
    /// </summary>
    public class DashBoardData
    {
        public List<DashBoardItems> Items { get; set; }
    }
}
