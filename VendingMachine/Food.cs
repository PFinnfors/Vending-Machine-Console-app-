using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Food : Items
    {
        public Food(string foodName, int foodPrice, string foodDescription, string foodUseMsg, int foodCount)
            : base(foodName, foodPrice, foodDescription, foodUseMsg, foodCount)
        {
            ItemName = foodName;
            ItemPrice = foodPrice;
            ItemDescription = foodDescription;
            ItemUseMsg = foodUseMsg;
            ItemCount = foodCount;
        }
    }
}
