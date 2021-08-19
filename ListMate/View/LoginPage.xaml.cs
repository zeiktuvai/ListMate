using System;
using ListMate.Provider;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListMate.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void bttnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {               
                await FirebaseProvider.AuthenticateToFirebaseAsync(tbxUserName.Text, tbxUserPass.Text);

                if (TokenProvider.FbAuthToken != null)
                {
                    Application.Current.Properties["SignedIn"] = "true";
                    await Application.Current.SavePropertiesAsync();
                    try
                    {
                        await SecureStorage.SetAsync("UserName", tbxUserName.Text);
                        await SecureStorage.SetAsync("Pass", tbxUserPass.Text);

                        MasterDetailPage lp = new MasterDetailPage();
                        lp.Master = new LandingPageMaster();
                        lp.Detail = new NavigationPage(new LandingPageDetail());
                        Application.Current.MainPage = lp;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception em)
            {
                await DisplayAlert("Error", "Login unsuccesful: " + em.InnerException.Message, "Ok");                
            }
            
        }

        private void bttnTestCred_Clicked(object sender, EventArgs e)
        {
        }

        private void bttnCreateAcct_Clicked(object sender, EventArgs e)
        {
            //TODO: Create sign up modal
            throw new NotImplementedException();
        }
    }
}
