using System;
namespace NamingConvention.Models.ResponseModels
{
    /// <summary>
    /// DashBoard Response
    /// </summary>
    public class DashBoardResponse : BaseResponse
    {
        public int News { get; set; }
        public int Circular { get; set; }
        public int HomeWork { get; set; }
    }
}
