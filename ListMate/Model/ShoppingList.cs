using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace ListMate.Model
{
    public class ShoppingList
    {
        public Guid ListID { get; set; }
        public string UserName { get; set; }
        public string ListName { get; set; }
        public List<string> UserIdSharedWith { get; set; }

        public static List<ShoppingList> ConvertItems(IReadOnlyCollection<FirebaseObject<ShoppingList>> list)
        {
            var ProcList = new List<FirebaseObject<ShoppingList>>(list);
            var FilteredList = ProcList.FindAll(a => a.Object.ListName != null);
            return FilteredList.ConvertAll(item => new ShoppingList
            {
                ListID = item.Object.ListID,
                ListName = item.Object.ListName,
                UserName = item.Object.UserName,
                UserIdSharedWith = item.Object.UserIdSharedWith
            });

        }
    }
}
