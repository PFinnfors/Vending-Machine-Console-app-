using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    abstract class Items
    {
        //Constructor
        internal Items(string itemName, int itemPrice, string itemDescription, string itemUseMsg, int itemCount)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemDescription = itemDescription;
            ItemUseMsg = itemUseMsg;
            ItemCount = itemCount;
        }

        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public string ItemUseMsg { get; set; }
        public int ItemCount { get; set; }

        //Examines items in the machine
        public virtual void Examine()
        {
            Machine.WriteColor("Yellow", ItemDescription);
        }

        //Purchases items in the machine
        public virtual void Buy()
        {
            Machine.WriteColor("Green", "You bought the " + ItemName + "!");
        }

        //Uses items purchased in the machine
        public virtual void Use()
        {
            Machine.WriteColor("Green", ItemUseMsg);
        }
    }
}
