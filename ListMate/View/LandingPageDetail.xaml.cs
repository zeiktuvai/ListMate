using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ListMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPageDetail : ContentPage
    {
        public LandingPageDetail()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCrBxHkFPp-9G7prUOXFs4bUHJF8AzS0fk"));

            var auth = await authProvider.SignInWithEmailAndPasswordAsync("test@icomnetworks.net", "BL@ckH@wk2142");

            
            var fireb = new FirebaseClient("https://listmate-40330-default-rtdb.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
            });


        }
    }
}