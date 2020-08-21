using System;
using System.Collections.Generic;
using NamingConvention.Views.Base;
using Xamarin.Forms;

namespace NamingConvention.Views.Social
{
    public partial class SocialLogin : BaseContentPage
    {
        public string ProviderName { get; set; }
        public SocialLogin(string _providername)
        {
            InitializeComponent();
            ProviderName = _providername;
        }
    }
}
