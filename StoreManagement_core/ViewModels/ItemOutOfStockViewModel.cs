using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.ViewModels
{
    public class ItemOutOfStockViewModel
    {

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public DateTime StockedDate { get; set; }

    }
}
