using System;
using ListMate.Provider;
using ListMate.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListMate.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateShoppingList : ContentPage
    {
        public CreateShoppingList()
        {
            InitializeComponent();
        }

        private async void bttnCancelList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void bttnSaveList_Clicked(object sender, EventArgs e)
        {
            if (tbxListName.Text != null)
            {
                FirebaseProvider firebase = new FirebaseProvider();
                await firebase.connectFireDB();
                try
                {
                    await firebase.CreateListItem(new ShoppingList { ListID = Guid.NewGuid(), ListName = tbxListName.Text, UserName = TokenProvider.FbAuthToken.User.Email });
                    await Navigation.PopModalAsync();
                }
                catch
                {
                    await DisplayAlert("Existing List", "That list already exists!", "OK");
                }            
            }
        }
    }
}