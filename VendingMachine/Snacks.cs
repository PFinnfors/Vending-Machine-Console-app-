using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Snacks : Items
    {
        public Snacks(string snackName, int snackPrice, string snackDescription, string snackUseMsg, int snackCount)
            : base(snackName, snackPrice, snackDescription, snackUseMsg, snackCount)
        {
            ItemName = snackName;
            ItemPrice = snackPrice;
            ItemDescription = snackDescription;
            ItemUseMsg = snackUseMsg;
            ItemCount = snackCount;
        }
    }
}
