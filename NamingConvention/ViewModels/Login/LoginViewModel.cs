using System.Threading.Tasks;
using System.Windows.Input;
using NamingConvention.Models.RequestModels;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Service;
using NamingConvention.Utilities;
using NamingConvention.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Forms;
using NamingConvention.Views.DashBoard;
using NamingConvention.Utilities.StaticAppResources;
using System;
using NamingConvention.Views.SideMenu;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NamingConvention.Views.Social;

namespace NamingConvention.ViewModels.Login
{
    /// <summary>
    /// Login View Model Calss
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Constructor
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAction());
            FacebookCommand = new Command(async () => await FaceBookLogin());
            GoogleCommand = new Command(async () => await GoogleLogin());
        }
        #endregion

        #region Command Declaration
        public ICommand LoginCommand { private set; get; }
        public ICommand FacebookCommand { private set; get; }
        public ICommand GoogleCommand { private set; get; }
        #endregion

        #region Variable Declaration

        public List<SchoolResponse> schoolListData;

        private ObservableCollection<SchoolResponse> schoolList;
        public ObservableCollection<SchoolResponse> SchoolList
        {
            get { return schoolList; }
            set
            {
                schoolList = value;
                OnPropertyChanged("SchoolList");
            }
        }
        private SchoolResponse selectedSchool;
        public SchoolResponse SelectedSchool
        {
            get { return selectedSchool; }
            set
            {
                selectedSchool = value;
                OnPropertyChanged("SelectedSchool");

            }
        }
        private string userName ;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion
        #region Command Methods

        /// <summary>
        /// Call for the Facebook Login and You need to use bundle id "Parsimo" to log in with facebook
        /// </summary>
        /// <returns></returns>
        async public Task FaceBookLogin()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SocialLogin("Facebook"));

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Constant.HideLoader();
                Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
            }
        }

        /// <summary>
        /// Call for the Google Login and You need to use bundle id "Parsimo" to log in with google
        /// </summary>
        /// <returns></returns>
        async public Task GoogleLogin()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SocialLogin("Google"));

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Constant.HideLoader();
                Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
            }
        }

        #endregion

        #region Methods
        public void SchoolListData()
        {
            SchoolList = new ObservableCollection<SchoolResponse>();
            SchoolList.Add(new SchoolResponse { SchoolName = "Vidhya Vihar", Id = 1 });
            SchoolList.Add(new SchoolResponse { SchoolName = "New Vidhya Vihar", Id = 2 });
        }
        async public Task GetSchoolList()
        {
            try
            {
                if (Constant.IsInternet)
                {
                    await Task.Run(() =>
                    {
                        Constant.ShowLoader(AppTexts.Loading);
                    });
                    var results = await APIService.Get(APIServicePath.GetSchools);

                    await Task.Run(() =>
                    {
                        Constant.HideLoader();
                    });
                    if (results.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrWhiteSpace(results.Content))
                    {
                        schoolListData = JsonConvert.DeserializeObject<List<SchoolResponse>>(results.Content);
                        if (schoolListData != null)
                        {
                            SchoolList = new ObservableCollection<SchoolResponse>();
                            foreach (var school in schoolListData)
                            {
                                SchoolList.Add(school);
                            }
                        }
                    }
                }
                else
                    Constant.DisplayAlert(AppTexts.NoInternet, AppTexts.OkButton, string.Empty);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                await Task.Run(() =>
                {
                    Constant.HideLoader();
                });
            }
        }
        async public Task LoginAction()
        {
            try
            {
                if (Constant.IsInternet)
                {
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        Constant.DisplayAlert(AppTexts.UserNameBlank, AppTexts.OkButton, string.Empty);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(Password))
                    {
                        Constant.DisplayAlert(AppTexts.PasswordBlank, AppTexts.OkButton, string.Empty);
                        return;
                    }
                    else if (SelectedSchool == null)
                    {
                        Constant.DisplayAlert(AppTexts.SelectSchool, AppTexts.OkButton, string.Empty);
                        return;
                    }

                    Constant.ShowLoader(AppTexts.Loading);
                    LoginRequest loginRequestData = new LoginRequest();
                    loginRequestData.UserName = UserName;
                    loginRequestData.Password = Password;
                    loginRequestData.SchoolId = SelectedSchool.Id;
                    loginRequestData.IsStudent = true;
                    string requestJson = JsonConvert.SerializeObject(loginRequestData);
                    CommonResponse response = await APIService.Post(APIServicePath.Login, requestJson);
                    if (response != null)
                    {
                        Constant.HideLoader();
                        Application.Current.MainPage = new NavigationPage(new MenuMasterPage());
                    }
                    else
                    {
                        Constant.HideLoader();
                        Application.Current.MainPage = new NavigationPage(new MenuMasterPage());
                    }
                }
                else
                    Constant.DisplayAlert(AppTexts.NoInternet, AppTexts.OkButton, string.Empty);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Constant.HideLoader();
                Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
            }
        }
        #endregion
    }
}
