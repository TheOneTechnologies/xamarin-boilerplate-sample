namespace NamingConvention.Service
{
    /// <summary>
    /// All API path 
    /// </summary>
    public class APIServicePath
    {
        #region Static Variable
        public static string BaseUrl = "http://127.0.0.1/";
        public static string Login = "Auth/Login";
        public static string GetDashboard = "Dashboards?lastCircularId={0}&lastNewsId={1}&lastHomeWorkId={2}";
        public static string GetSchools = "Schools";
        public static string FacebookAPIRequest = "https://graph.facebook.com/v2.7/me/?fields=name%2Cpicture%2Cwork%2Cwebsite%2Creligion%2Clocation%2Clocale%2Clink%2Ccover%2Cage_range%2Cbirthday%2Cdevices%2Cemail%2Cfirst_name%2Clast_name%2Cgender%2Chometown%2Cis_verified%2Clanguages&access_token=";
        public static string GoogleAPIRequest = "https://www.googleapis.com/plus/v1/people/me?access_token=";
        #endregion
    }
}
