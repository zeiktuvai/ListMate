using System;
using System.Windows.Input;
using ListMate.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ListMate.ViewModel;
using System.Threading.Tasks;
using System.IO;

namespace ListMate.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingItemDetail : ContentPage
    {
        public Guid ListGUID { get; set; }
        public ShoppingItem shoppingitem { get; set; }
        public ShoppingItemDetail(Guid listID, ShoppingItem item)
        {
            InitializeComponent();
            ListGUID = listID;
            shoppingitem = item;
            this.BindingContext = shoppingitem;
            if (shoppingitem.ItemImage != null)
            {
                var imgstream = new MemoryStream(shoppingitem.ItemImage);
                imgItemPicture.Source = ImageSource.FromStream(() => imgstream);
            }
            
        }
        protected async override void OnAppearing()
        {
            await updatePageAsync();
        }

        private async void bttnClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private void bttnItemLink_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(shoppingitem.ItemURL));
        }

        private async void bttnEditItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateShoppingItem(ListGUID, shoppingitem)));
            await updatePageAsync();
        }

        private async void bttnDelItem_Clicked(object sender, EventArgs e)
        {
            var UserInput = await DisplayAlert("Delete?", "Do you wish to delete " + shoppingitem.ItemName + "?", "Yes", "No");
            if (UserInput == true)
            {
                ShoppingItemViewModel.DeleteItemAsync(shoppingitem);
                await Navigation.PopModalAsync();
            }
        }

        private async Task updatePageAsync()
        {
            shoppingitem = await ShoppingItemViewModel.GetShoppingItem(shoppingitem);
           
        }

        private async void bttnPurchaseItem_Clicked(object sender, EventArgs e)
        {
            shoppingitem.ItemPurchased = !shoppingitem.ItemPurchased;
            ShoppingItemViewModel.UpdateItemAsync(shoppingitem);
            await updatePageAsync();
        }
    }
}