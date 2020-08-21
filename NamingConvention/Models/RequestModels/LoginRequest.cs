namespace NamingConvention.Models.RequestModels
{
    /// <summary>
    /// Login Request Model
    /// </summary>
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SchoolId { get; set; }
        public bool IsStudent { get; set; }
    }
}