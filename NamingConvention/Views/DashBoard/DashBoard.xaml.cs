using NamingConvention.ViewModels.Dashboard;
using NamingConvention.Views.Base;
using NamingConvention.Views.SideMenu;

namespace NamingConvention.Views.DashBoard
{
    /// <summary>
    /// DashBoard View
    /// </summary>
    public partial class DashBoard : BaseContentPage
    {
        #region Local Variable
        private DashBoardViewModel dashBoardViewModel;
        #endregion

        #region Constructor
        public DashBoard()
        {
            InitializeComponent();
            BindingContext = dashBoardViewModel = new DashBoardViewModel();
        }

        async protected override void OnAppearing()
        {
            await dashBoardViewModel.ReadJsonDataSetDashBoardCount();
            
            base.OnAppearing();
        }
        #endregion
    }
}
