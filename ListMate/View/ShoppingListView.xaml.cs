using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListMate.Provider;
using ListMate.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using ListMate.ViewModel;

namespace ListMate.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListView : ContentPage
    {
        public Guid ListGUID { get; set; }
        public string ListName { get; set; }
        public FirebaseProvider firebase { get; set; }
        public ShoppingListView()
        {
            InitializeComponent();
            
        }

        public ShoppingListView(LandingPageMasterMenuItem selectedItem)
        {
            InitializeComponent();
            ListGUID = selectedItem.ListID;
            ListName = selectedItem.Title;
            lblListName.Text = ListName;
            this.Title = "";
        }

        protected override void OnAppearing()
        {
            lstItems.IsGroupingEnabled = true;
            UpdateListView();
            
        }

        private async void UpdateListView()
        {
            firebase = new FirebaseProvider();
            firebase.connectFireDB();

            var ordered = ShoppingItemViewModel.ConvertItems(await firebase.GetListTable<ShoppingItem>(), ListGUID).OrderBy(n => n.ItemName);
            var grouped = ordered.GroupBy(g => g.ItemCategory).OrderBy(o => o.Key).ToList();
            lstItems.ItemsSource = grouped;
            lblTotCost.Text = ordered.Sum(t => t.ItemPrice).ToString("$.00");
            lblItemCount.Text = ordered.Count().ToString();
        }

        private async void bttnDelList_Clicked(object sender, EventArgs e)
        {
            var userInp = await DisplayAlert("Delete List?", "Are you sure you want to delete " + this.Title, "Yes", "No");
            if (userInp == true)
            {
                var table = await firebase.GetListTable<ShoppingItem>();
                foreach (var item in table)
                {
                    if (item.Object.List_ID == ListGUID.ToString())
                    {
                        await firebase.DeleteItem(item.Key);
                    }
                }
                var listid = await firebase.GetListTable<ShoppingList>();
                var test = listid.First(a => a.Object.ListID == ListGUID).Key;

                await firebase.DeleteItem(listid.First(a => a.Object.ListID == ListGUID).Key);

                App.Current.MainPage = new LandingPage();
            }
        }

        private async void lstItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {            
            await Navigation.PushModalAsync(new NavigationPage(new ShoppingItemDetail(ListGUID, ((sender as ListView).SelectedItem as ShoppingItem))));
            UpdateListView();
        }

        private async void bttnAddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateShoppingItem(ListGUID)));
            UpdateListView();
        }
    }
}