using System;
using System.Collections.Generic;
using NamingConvention.ViewModels.PhotoGallery;
using NamingConvention.Views.Base;
using Xamarin.Forms;

namespace NamingConvention.Views.PhotoGallary
{
    public partial class PhotoGallary : BaseContentPage
    {
        #region Local Variable
        private PhotoGalleryViewModel photoGalleryViewModel;
        #endregion
        #region Constructor
        public PhotoGallary()
        {
            InitializeComponent();
            BindingContext = photoGalleryViewModel = new PhotoGalleryViewModel();
        }
        #endregion
    }
}
