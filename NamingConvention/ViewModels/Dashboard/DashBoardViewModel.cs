using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Service;
using NamingConvention.Utilities;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.ViewModels.Base;
using NamingConvention.Views.DashBoard;
using NamingConvention.Views.PhotoGallary;
using NamingConvention.Views.SideMenu;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NamingConvention.ViewModels.Dashboard
{
    /// <summary>
    /// DashBoard View Model Class
    /// </summary>
    public class DashBoardViewModel: BaseViewModel
    {
        #region Local Variable
        public ObservableCollection<DashBoardItems> DashBoardOptions { get; set; }
        #endregion
       
        #region Constructor
        public DashBoardViewModel()
        {
            DashBoardOptions = new ObservableCollection<DashBoardItems>();
        }
        #endregion
     
        #region Methods

        /// <summary>
        /// Call when Dashboard Item will click.
        /// </summary>
        public Command DashBoardItemClickCommand
        {
            get
            {
                return new Command((Option) =>
                {
                    //Selected option
                    DashBoardItems selectedOption = (DashBoardItems)Option;
                });
            }
        }
        /// <summary>
        /// Read Json Data
        /// </summary>
        async public Task ReadJsonDataSetDashBoardCount()
        {
            DashBoardOptions.Clear();
            var resourcePrefix = "NamingConvention.Utilities.StaticAppResources";
            var assembly = typeof(DashBoard).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{resourcePrefix}.HomeMenuItems.json");
            string text = string.Empty;
            if (stream != null)
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                DashBoardData dashBoardResult = JsonConvert.DeserializeObject<DashBoardData>(text);
                if (dashBoardResult.Items.Count > 0)
                {
                    foreach (var option in dashBoardResult.Items)
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            option.Radius = 55;
                            option.Height = 80;
                        }
                        else
                        {
                            option.Radius = 100;
                            option.Height = 75;
                        }
                        DashBoardOptions.Add(option);
                    }
                }
            }

        }

        /// <summary>
        /// Set DashBoard Count.
        /// </summary>
        /// <returns></returns>
        private async Task SetDashBoardCount()
        {
            try
            {
                Constant.ShowLoader(AppTexts.Loading);
                if (Constant.IsInternet)
                {
                    var results = await APIService.Get(string.Format(APIServicePath.GetDashboard, 0, 0, 0));
                    if (!string.IsNullOrWhiteSpace(results.Content))
                    {
                        DashBoardResponse dashBoardResponse = JsonConvert.DeserializeObject<DashBoardResponse>(results.Content);
                        if (dashBoardResponse != null)
                        {
                            if (dashBoardResponse.Errors != null && dashBoardResponse.Errors.Count > 0)
                                //Error
                                Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
                            else
                            {
                                foreach (var option in DashBoardOptions)
                                {
                                    if (option.Title == "Homework")
                                    {
                                        if (dashBoardResponse.HomeWork > 0)
                                        {
                                            option.UnreadCountVisible = true;
                                            option.UnReadCount = dashBoardResponse.HomeWork;
                                        }
                                    }
                                    else if (option.Title == "Birthdays")
                                    {
                                        if (dashBoardResponse.Circular > 0)
                                        {
                                            option.UnreadCountVisible = true;
                                            option.UnReadCount = dashBoardResponse.Circular;
                                        }
                                    }
                                    else if (option.Title == "News")
                                    {
                                        if (dashBoardResponse.News > 0)
                                        {
                                            option.UnreadCountVisible = true;
                                            option.UnReadCount = dashBoardResponse.News;
                                        }
                                    }
                                }
                                Constant.HideLoader();
                            }
                        }
                        else
                        {
                            Constant.HideLoader();
                            Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
                        }
                    }
                    else
                    {
                        Constant.HideLoader();
                        //Constant.DisplayAlert(AppTexts.SomethingWentWrong, AppTexts.OkButton, string.Empty);
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
