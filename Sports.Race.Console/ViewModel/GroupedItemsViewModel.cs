using System.Collections.Generic;
using Sports.Wpf.Common.DataModel;
using Sports.Wpf.Common.ViewModel;

namespace Sports.Race.Console.ViewModel
{
    //A View Model for a GroupedItemsPage
    public class GroupedItemsViewModel : GroupedItemsViewModelBase
    {
        public string Title => "Race Console";

        protected override IEnumerable<UIDataItem> GetItems()
        {
            return null;
        }
    }
}