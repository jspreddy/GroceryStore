using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class HomePageViewModel
    {
        public IList<InventoryViewModel> InventoryList;
        public InventoryViewModel CreateInventoryItem;
    }
}