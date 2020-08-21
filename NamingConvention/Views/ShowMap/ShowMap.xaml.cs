using System;
using System.Collections.Generic;
using NamingConvention.Utilities;
using NamingConvention.Utilities.StaticAppResources;
using NamingConvention.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NamingConvention.Views.ShowMap
{
    public partial class ShowMap : BaseContentPage
    {

        #region Constructor
        public ShowMap()
        {
            InitializeComponent();
        }

        #endregion

        #region OnAppear

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            //Get Current location of user
            var currentLocation = await Constant.GetUserCurrentLocation();
            if (currentLocation != null)
            {
                mymap.IsVisible = true;
                Position position = new Position(currentLocation.Latitude, currentLocation.Longitude);

                //Set map region with zooming distance.
                mymap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(200)));

                //Remove the old pins.
                if (mymap.Pins.Count > 0)
                {
                    mymap.Pins.Clear();
                }

                //Add new location pin
                var CurrentMapPin = new Pin
                {

                    Type = PinType.Place,
                    Label = string.Empty,
                    Position = position,

                };

                if (CurrentMapPin != null)
                    mymap.Pins.Add(CurrentMapPin);
            }
            else
                Constant.DisplayAlert(AppTexts.LocationNotFound, AppTexts.OkButton, string.Empty);
        }

        #endregion

    }
}
