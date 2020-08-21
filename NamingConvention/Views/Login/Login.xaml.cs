using NamingConvention.ViewModels.Login;
using NamingConvention.Views.Base;

namespace NamingConvention.Views.Login
{
    /// <summary>
    /// Login View
    /// </summary>
    public partial class Login : BaseContentPage
    {
        #region Local Variable
       private LoginViewModel loginViewModel;
        #endregion

        #region Constructor
        public Login()
        {
            InitializeComponent();
            BindingContext = loginViewModel = new LoginViewModel();
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            if (loginViewModel.schoolListData == null)
                loginViewModel.SchoolListData();
        }
        #endregion
    }
}
