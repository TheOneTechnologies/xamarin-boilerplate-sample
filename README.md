# Xamarin-boilerplate-sample
*Ready to use code for any Xamarin.Forms startup project with MVVM structure.*
*Boilerplate sample Xamarin Mobile App framework used by The One Technologies.*
## Clone the project using the git url  
```sh
$ git clone https://github.com/TheOneTechnologies/xamarin-boilerplate-sample
``` 
## NuGet packages used
* [Acr.UserDialogs](https://www.nuget.org/packages/Acr.UserDialogs)  
* [DLToolkit.Forms.Controls.FlowListView](https://www.nuget.org/packages/DLToolkit.Forms.Controls.FlowListView)
* [Plugin.Connectivity](https://www.nuget.org/packages/Plugin.Connectivity)
* [Plugin.FirebasePushNotification](https://www.nuget.org/packages/Plugin.FirebasePushNotification)
* [Plugin.Permissions](https://www.nuget.org/packages/Plugin.Permissions)
* [Xam.Plugin.Connectivity](https://www.nuget.org/packages/Xam.Plugin.Connectivity)
* [Xam.Plugin.Geolocator](https://www.nuget.org/packages/Xam.Plugin.Geolocator)
* [Xam.Plugin.Media](https://www.nuget.org/packages/Xam.Plugin.Media)
* [Xamarin.Auth](https://www.nuget.org/packages/Xamarin.Auth)
* [Xamarin.Forms.Maps](https://www.nuget.org/packages/Xamarin.Forms.Maps)
* [Xamarin.Forms.PancakeView](https://www.nuget.org/packages/Xamarin.Forms.PancakeView)
* [Xamarin.UITest ](https://www.nuget.org/packages/Xamarin.UITest)
* [NUnit](https://www.nuget.org/packages/NUnit)
# Basic screen UI  
* Splash screen
* Login screen
* Dashboard screen with Tabbed and Master-Detail page 
* Map screen
* Download media screen
* pick photo from gallary/camera
## This project included below ready to use code
* UITest 
* Side menu intigration
* Tabbed page intigration(with bottom tabbed customisation in Android)
* Download media
* Http Get and post methods with authorisation
* Internet connectivity check
* Facebook and Google login
* View Effects
* Follow C# naming standard
* Global style and fonts for an application
* Carousel Image list
* Basic renderer for iOS and Android
* Follow basic MVVM structure
* Firebase push notifications
* Basic c# Event handler
* Xamarin forms map intigration
* Run time permission
* Get Device information
* Login flow
* Login validations
* Pick image form gallary or camera
* Use Custom views in Xaml page 

### Firebase push notification intigration steps
* follow the google firebase setup from below link
    *  https://github.com/CrossGeeks/FirebasePushNotificationPlugin/blob/master/docs/FirebaseSetup.md
* Android setup
    * UnComment firebase code from OnStart() methods in NamingConvention.App.App.cs file
* iOS setup
    * UnComment firebase code from FinishedLaunching() methods in NamingConvention.iOS.AppDelegate.cs file
* Then, try to send test notification from firebase 

License
----
The One Technologies