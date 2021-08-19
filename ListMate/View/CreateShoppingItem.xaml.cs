using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using ListMate.Model;
using ListMate.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace ListMate.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]    
    public partial class CreateShoppingItem : ContentPage
    {
        public Guid ListID { get; set; }
        public bool _isEdit { get; set; }
        public ShoppingItem ItemToEdit { get; set; }
        public CreateShoppingItem(Guid listID)
        {
            InitializeComponent();
            ListID = listID;
            _isEdit = false;
        }
        public CreateShoppingItem(Guid listID, ShoppingItem item)
        {
            InitializeComponent();
            _isEdit = true;
            ListID = listID;
            ItemToEdit = item;
            this.Title = "Edit " + item.ItemName;
            
            tbxItemName.Text = item.ItemName;
            tbxItemCat.Text = item.ItemCategory;
            tbxItemPrice.Text = item.ItemPrice.ToString();
            tbxItemQty.Text = item.ItemQty.ToString();
            tbxItemStore.Text = item.ItemStore;
            tbxItemURL.Text = item.ItemURL;
        }

        private async void bttnCancelItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void bttnSaveItem_Clicked(object sender, EventArgs e)
        {
            if (_isEdit == false)
            {
                if (tbxItemName.Text != null && tbxItemPrice.Text != null && tbxItemQty.Text != null)
                {
                    ShoppingItem newItem = new ShoppingItem()
                    {
                        ItemName = tbxItemName.Text,
                        ItemPrice = double.Parse(tbxItemPrice.Text),
                        ItemQty = int.Parse(tbxItemQty.Text),
                        ItemStore = tbxItemStore.Text,
                        ItemURL = tbxItemURL.Text,
                        ItemCategory = tbxItemCat.Text,
                        ItemID = Guid.NewGuid(),
                        ItemPurchased = false,
                        ItemSelect = false,
                        ItemImage = null,
                        ItemVer = DateTime.Now,
                        List_ID = ListID.ToString()
                    };
                    try
                    {
                        ShoppingItemViewModel.CreateItemAsync(newItem);
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Error", "An error occured trying to create the item", "OK");   
                    }
                    await Navigation.PopModalAsync();
                }
            } else
            {
                if (tbxItemName.Text != null && tbxItemPrice.Text != null && tbxItemQty.Text != null)
                {
                    ShoppingItem updateItem = new ShoppingItem()
                    {
                        ItemName = tbxItemName.Text,
                        ItemPrice = double.Parse(tbxItemPrice.Text),
                        ItemQty = int.Parse(tbxItemQty.Text),
                        ItemStore = tbxItemStore.Text,
                        ItemURL = tbxItemURL.Text,
                        ItemCategory = tbxItemCat.Text,
                        ItemID = ItemToEdit.ItemID,
                        ItemImage = ItemToEdit.ItemImage,
                        ItemPurchased = ItemToEdit.ItemPurchased,
                        ItemSelect = ItemToEdit.ItemSelect,
                        ItemVer = ItemToEdit.ItemVer,
                        List_ID = ItemToEdit.List_ID
                    };

                    if (tbxImageUrl.Text != null)
                    {
                        updateItem.ItemImage = await GetImageFromURL(tbxImageUrl.Text); ;
                    }

                    try
                    {
                        ShoppingItemViewModel.UpdateItemAsync(updateItem);
                    }
                    catch (Exception)
                    {


                    }

                    await Navigation.PopModalAsync();
                }
            }
        }

        private async Task<byte[]> GetImageFromURL(string url)
        {
            //TODO: Extract
            if (tbxImageUrl.Text != null)
            {
                if (tbxImageUrl.Text.Contains("jpg"))
                {
                    var httpclient = new HttpClient();
                    httpclient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

                    byte[] image = await httpclient.GetByteArrayAsync(url);
                    return image;
                } else { await DisplayAlert("Error", "Only JPG files are supported at this time.", "Ok"); }

            } else
            {
                await DisplayAlert("Error", "Please enter image URL.", "Ok");
            }

            return null;
            //var webClient = new WebClient();
            //byte[] imageBytes = webClient.DownloadData(tbxImageUrl.Text);
            //return imageBytes;
        }

        private async void bttnSave_Clicked(object sender, EventArgs e)
        {
            var test = await GetImageFromURL(tbxImageUrl.Text);

            //string url;
            //string download = null;
            
            
            
            //if (tbxItemURL.Text.Contains("?"))
            //{
            //    url = tbxItemURL.Text.Substring(0, tbxItemURL.Text.IndexOf("?"));
            //} else
            //{
            //    url = tbxItemURL.Text;
            //}

            //var httpclient = new HttpClient();
            //httpclient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            //try
            //{
            //    download = await httpclient.GetStringAsync(url);
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("404"))
            //    {
            //        await DisplayAlert("Error", "Server returned 404 error, check to make sure the item URL is correct.  If it is that item may no longer be avaialble.", "ok");

            //    }

            //}

            //if (download != null)
            //{               
            //    List<string> test1 = new List<string>(download.Split('<'));
            //    var test2 = test1.FindAll(a => a.Contains("jpg")).FindAll(b => b.Contains("http")).FindAll(c => !c.Contains("script"));
            //    var test3 = test2.Find(b => b.Contains("image") || b.Contains("img"));
            //    var test4 = test3.Substring(test3.IndexOf("http"), test3.Substring(test3.IndexOf("http")).IndexOf("jpg")+3);

            //    //var t1 = test1.FindAll(a1 => a1.;
            //}
        }
    }
}

