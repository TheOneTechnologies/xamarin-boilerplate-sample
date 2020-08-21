using System.Collections.Generic;

namespace NamingConvention.Models.ResponseModels
{
    /// <summary>
    /// BaseResponse class
    /// </summary>
    public class BaseResponse
    {
        public IList<Errors> Errors { get; set; }
    }
    /// <summary>
    /// Errors class
    /// </summary>
    public class Errors
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
