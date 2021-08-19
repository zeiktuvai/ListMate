using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListMate.Model
{
    public class ShoppingItem
    {
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int ItemQty { get; set; }
        public string ItemStore { get; set; }
        public string ItemURL { get; set; }
        public bool ItemPurchased { get; set; }
        public bool ItemSelect { get; set; }
        public byte[] ItemImage { get; set; }
        public DateTime ItemVer { get; set; }
        public string List_ID { get; set; }
        public string ItemCategory { get; set; }
        public ImageSource _ProductImage { get; set; }


    }
}
