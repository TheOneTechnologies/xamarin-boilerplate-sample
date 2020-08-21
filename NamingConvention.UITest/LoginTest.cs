using System;
using NUnit.Framework;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace NamingConvention.UITest
{
    public class LoginTest
    {
        readonly Query emailField;
        readonly Query passwordField;
        readonly Query signInButton;
        Platform platform;
        IApp app;
        public LoginTest(Platform platform, IApp app)
        {
            this.platform = platform;
            this.app = app;
            emailField = x => x.Marked("UserName");
            passwordField = x => x.Marked("Password");
            signInButton = x => x.Marked("Login");
        }
        public LoginTest EnterCredentials(string username, string password)
        {
            app.WaitForElement(emailField);
            app.Tap(emailField);
            app.EnterText(username);
            app.DismissKeyboard();

            app.Tap(passwordField);
            app.EnterText(password);
            app.DismissKeyboard();

            app.Screenshot("Credentials Entered");

            return this;
        }

        public LoginTest SignIn()
        {
            app.Tap(signInButton);
            Assert.Pass("we have succefully Logged In");
            return this;
        }

        public void CheckThereIsNoNavigation()
        {
            app.Tap(signInButton);
            app.WaitForElement(emailField);
            app.WaitForElement(passwordField);
            app.WaitForElement(signInButton);
        }
    }
}
