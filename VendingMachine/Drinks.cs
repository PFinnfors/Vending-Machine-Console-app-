using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Drinks : Items
    {
        public Drinks(string drinkName, int drinkPrice, string drinkDescription, string drinkUseMsg, int drinkCount)
            : base(drinkName, drinkPrice, drinkDescription, drinkUseMsg, drinkCount)
        {
            ItemName = drinkName;
            ItemPrice = drinkPrice;
            ItemDescription = drinkDescription;
            ItemUseMsg = drinkUseMsg;
            ItemCount = drinkCount;
        }
    }
}
