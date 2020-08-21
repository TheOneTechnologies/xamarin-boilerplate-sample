using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NamingConvention.Views.SideMenu;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.Base
{
    /// <summary>
    /// Base View Model with INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Local Variable
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructors
        public BaseViewModel()
        {
            BackCommand = new Command(() => GoBack());
            menuCommand = new Command(Menuaction);
        }
        #endregion

        #region Command Declaration
        public ICommand BackCommand { private set; get; }
        public ICommand menuCommand { private set; get; }
        #endregion

        #region Command Methods

        public void Menuaction()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MenuMasterPage.masterPage.IsPresented = !MenuMasterPage.masterPage.IsPresented;
            });
        }

        private void GoBack()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }
        #endregion

    }
}

