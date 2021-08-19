using ListMate.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListMate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("SignedIn"))
            {
                if (Application.Current.Properties["SignedIn"].ToString() == "true")
                {
                    MainPage = new LandingPage();
                }
                else
                {
                    MainPage = new LoginPage();
                }
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
            //TODO: Login code here
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
