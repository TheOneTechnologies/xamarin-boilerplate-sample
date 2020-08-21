using System.Net;

namespace NamingConvention.Models.ResponseModels
{
    /// <summary>
    /// Common Response Model
    /// </summary>
    public class CommonResponse
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
