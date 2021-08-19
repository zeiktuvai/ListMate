using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using ListMate.Model;
using ListMate.Provider;
using Xamarin.Forms;

namespace ListMate.ViewModel
{
    class ShoppingItemViewModel
    {
        public static List<ShoppingItem> ConvertItems(IReadOnlyCollection<FirebaseObject<ShoppingItem>> list, Guid ListID)
        {
            var ProcList = new List<FirebaseObject<ShoppingItem>>(list);
            var FilteredList = ProcList.FindAll(a => a.Object.List_ID == ListID.ToString());
            return FilteredList.ConvertAll(item => new ShoppingItem
            {
                ItemID = item.Object.ItemID,
                ItemName = item.Object.ItemName,
                ItemPrice = item.Object.ItemPrice,
                ItemPurchased = item.Object.ItemPurchased,
                ItemQty = item.Object.ItemQty,
                ItemSelect = item.Object.ItemSelect,
                ItemStore = item.Object.ItemStore,
                ItemURL = item.Object.ItemURL,
                ItemVer = item.Object.ItemVer,
                ItemImage = item.Object.ItemImage,
                List_ID = item.Object.List_ID,
                ItemCategory = item.Object.ItemCategory,
                _ProductImage = item.Object.ItemImage != null ? ImageSource.FromStream(() => new MemoryStream(item.Object.ItemImage)) : null 
        });
        }
        public static ShoppingItem ConvertItems(FirebaseObject<ShoppingItem> item)
        {
            return new ShoppingItem()
            {
                ItemID = item.Object.ItemID,
                ItemName = item.Object.ItemName,
                ItemPrice = item.Object.ItemPrice,
                ItemPurchased = item.Object.ItemPurchased,
                ItemQty = item.Object.ItemQty,
                ItemSelect = item.Object.ItemSelect,
                ItemStore = item.Object.ItemStore,
                ItemURL = item.Object.ItemURL,
                ItemVer = item.Object.ItemVer,
                ItemImage = item.Object.ItemImage,
                List_ID = item.Object.List_ID,
                ItemCategory = item.Object.ItemCategory,
                _ProductImage = item.Object.ItemImage != null ? ImageSource.FromStream(() => new MemoryStream(item.Object.ItemImage)) : null
            };
        }

        public static async void CreateItemAsync(ShoppingItem item)
        {
            FirebaseProvider firebase = new FirebaseProvider();
            firebase.connectFireDB();
            try
            {
                await firebase.CreateListItem(new ShoppingItem {
                    ItemCategory = item.ItemCategory,
                    ItemID = item.ItemID,
                    ItemImage = item.ItemImage,
                    ItemName = item.ItemName,
                    ItemPrice = item.ItemPrice,
                    ItemQty = item.ItemQty,
                    ItemStore = item.ItemStore,
                    ItemURL = item.ItemURL,
                    ItemPurchased = item.ItemPurchased,
                    ItemSelect = item.ItemSelect,
                    ItemVer = item.ItemVer,
                    List_ID = item.List_ID
                });
            }
            catch
            {
                throw new Exception("Error creating item.");
            }
        }
        public static async void DeleteItemAsync(ShoppingItem item)
        {
            FirebaseProvider firebase = new FirebaseProvider();
            firebase.connectFireDB();
            try
            {
                await firebase.DeleteItem(await GetItemKeyAsync(item));
            }
            catch (Exception)
            {
                                
            }
        }
        public static async void UpdateItemAsync(ShoppingItem item)
        {
            FirebaseProvider firebase = new FirebaseProvider();
            firebase.connectFireDB();
            try
            {
                await firebase.UpdateItem(item, await GetItemKeyAsync(item));
            }
            catch
            {
                throw new Exception("Error updating item.");
            }
        }
        public static async Task<string> GetItemKeyAsync(ShoppingItem shopitem)
        {
            FirebaseProvider firebase = new FirebaseProvider();
            firebase.connectFireDB();
            var table = await firebase.GetListTable<ShoppingItem>();
            foreach (var item in table)
            {
                if (item.Object.ItemID.ToString() == shopitem.ItemID.ToString())
                {
                    return item.Key.ToString();
                } 
            }
            return "Key Not Found";
        }

        public static async Task<ShoppingItem> GetShoppingItem(ShoppingItem item)
        {
            FirebaseProvider firebase = new FirebaseProvider();
            firebase.connectFireDB();
            return await firebase.GetItem<ShoppingItem>(await GetItemKeyAsync(item));            
        }
    }
}
