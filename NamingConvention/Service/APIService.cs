using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NamingConvention.Models.ResponseModels;
using NamingConvention.Utilities;

namespace NamingConvention.Service
{
    public class APIService
    {
        #region Static Methods
        /// <summary>
        /// Static Http Client
        /// </summary>
        private static HttpClient BaseHttpClient
        {
            get
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(60);
                client.BaseAddress = new Uri(APIServicePath.BaseUrl);
                if (Constant.LoginResponse != null && !string.IsNullOrWhiteSpace(Constant.LoginResponse.Access_Token))
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constant.LoginResponse.Access_Token);
                return client;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Http Post method 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="requestJSON"></param>
        /// <returns></returns>
        public static async Task<CommonResponse> Post(string methodName, string requestJSON)
        {
            CommonResponse commonResponseModel = new CommonResponse();
            try
            {
                var response = await BaseHttpClient.PostAsync(methodName, new StringContent(requestJSON, Encoding.UTF8, "application/json"));
                string jsonResponse = await response.Content.ReadAsStringAsync();
                commonResponseModel.Content = jsonResponse;
                commonResponseModel.StatusCode = response.StatusCode;
                return commonResponseModel;
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (OperationCanceledException ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return commonResponseModel;
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return commonResponseModel;
            }
        }

        public static async Task<CommonResponse> Get(string methodName)
        {
            CommonResponse responseModel = new CommonResponse();
            try
            {
                var response = await BaseHttpClient.GetAsync(methodName);
                var data = response.Content.ReadAsStringAsync().Result;
                responseModel.StatusCode = response.StatusCode;
                responseModel.Content = data;
                return responseModel;
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (OperationCanceledException ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return responseModel;
            }
            #pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
            #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return responseModel;
            }
        }
        #endregion
    }
}
