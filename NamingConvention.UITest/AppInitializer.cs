using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace NamingConvention.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
                return ConfigureApp.Android.StartApp();
            return ConfigureApp.iOS.InstalledApp("com.theone.namingconvention").DeviceIdentifier("65E94D9D-C9F2-4ED5-85B8-F85759111297").StartApp();
        }
    }
}
