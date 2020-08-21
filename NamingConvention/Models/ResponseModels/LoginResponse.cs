namespace NamingConvention.Models.ResponseModels
{
    /// <summary>
    /// LoginResponse Model
    /// </summary>
    public class LoginResponse : BaseResponse
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }
    }
}
