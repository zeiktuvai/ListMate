using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ListMate.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ListMate.Provider;
using ListMate.View;


namespace ListMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPageMaster : ContentPage
    {
        public ListView ListView;

        public LandingPageMaster()
        {
            InitializeComponent();
            BindingContext = new LandingPageMasterViewModel(null);
            ListView = MenuItemsListView;
        }

        protected override void OnAppearing()
        {
            UpdateMenuList();
        }
        private async void UpdateMenuList()
        {

                    ObservableCollection<LandingPageMasterMenuItem> menuList = new ObservableCollection<LandingPageMasterMenuItem>();
                    int menuId = 0;

                    FirebaseProvider firebase = new FirebaseProvider();
                    await firebase.connectFireDB();
                    
                    var list = ShoppingList.ConvertItems(await firebase.GetListTable<ShoppingList>());
                    if (list.Count > 0)
                    {
                        foreach (ShoppingList item in list)
                        {
                            menuList.Add(new LandingPageMasterMenuItem { Id = menuId, Title = item.ListName, TargetType = typeof(ShoppingListView), ListID = item.ListID });
                            menuId++;
                        }
                    }

                    BindingContext = new LandingPageMasterViewModel(menuList);
        }
        private async void bttnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateShoppingList()));
            UpdateMenuList();
        }

        class LandingPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<LandingPageMasterMenuItem> MenuItems { get; set; }

            public LandingPageMasterViewModel(ObservableCollection<LandingPageMasterMenuItem> menuList)
            {
                MenuItems = menuList;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        
    }
}