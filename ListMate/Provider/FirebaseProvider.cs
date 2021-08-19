using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth;
using System.Threading.Tasks;
using ListMate.Model;
using Xamarin.Essentials;
using Newtonsoft.Json;


namespace ListMate.Provider
{    
    public class FirebaseProvider
    {
        const string FirebaseUrl = "";
        string tablename = ""; // Helpers.RemoveSpecialCharacters(App.UserName) + "_List";
        public FirebaseClient conn { get; set; }     


        public async Task connectFireDB()
        {
            if (TokenProvider.FbAuthToken == null || TokenProvider.FbAuthToken.IsExpired())
            {
                try
                {
                    await AuthenticateToFirebaseAsync(await SecureStorage.GetAsync("UserName"), await SecureStorage.GetAsync("Pass"));
                }
                catch (Exception)
                {
                    throw new Exception("error logging in");                    
                }
            }            
                        
            conn = new FirebaseClient(FirebaseUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(TokenProvider.FbAuthToken.FirebaseToken)
            });

            tablename = TokenProvider.FbAuthToken.User.LocalId;
            //conn = new FirebaseClient(FirebaseUrl);             
        }
        public static async Task AuthenticateToFirebaseAsync(string UserName, string Password)
        {
            //TODO:  Sign out
            //TODO: Link accounts (allow other users to see linked content).
            FirebaseConfig FbCfg = new FirebaseConfig("AIzaSyCrBxHkFPp-9G7prUOXFs4bUHJF8AzS0fk");
            FirebaseAuthProvider FbAuthProv = new FirebaseAuthProvider(FbCfg);
            try
            {
               FirebaseAuthLink FbAuth = await FbAuthProv.SignInWithEmailAndPasswordAsync(UserName, Password);                
               TokenProvider.FbAuthToken = FbAuth;
            }
            catch (Exception e)
            {

                throw new Exception("Error Logging in", e);
            }
        }
        


        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetListTable<T>()
        {
            //check to see if this table based on the user name exists. if not create it.
            return (await conn.Child(tablename).OnceAsync<T>());
        }
        public async Task CreateListItem(object item)
        {
            if (item.GetType() == typeof(ShoppingList))
            {
                var checkList = ShoppingList.ConvertItems(await GetListTable<ShoppingList>()).Find(l => l.ListName == (item as ShoppingList).ListName);
                if (checkList == null)
                {
                    await conn.Child(tablename).PostAsync(item);
                } else
                {
                    throw new Exception("Shopping list exists already!");
                }
                
            } else
            {
                await conn.Child(tablename).PostAsync(item);
            }            
        }

        public async Task DeleteItem(string Key)
        {
            await conn.Child(tablename).Child(Key).DeleteAsync();
        }

        public async Task UpdateItem(object item, string Key)
        {
            if (item.GetType() == typeof(ShoppingItem))
            {
                await conn.Child(tablename).Child(Key).PutAsync<ShoppingItem>(item as ShoppingItem);
            }
        }

        public async Task<T> GetItem<T>(string Key)
        {
            return await conn.Child(tablename).Child(Key).OnceSingleAsync<T>();
        }
    }
}
