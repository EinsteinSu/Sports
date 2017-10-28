using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
using Sports.Race.Console.DataModel;
using Sports.Wpf.Common.DataModel;
using Sports.Wpf.Common.ViewModel;

namespace Sports.Race.Console.ViewModel
{
    //A View Model for a GroupedItemsPage
    public class GroupedItemsViewModel : GroupedItemsViewModelBase
    {
        public string Title => "Race Console";
        public new static ObservableCollection<UIDataItem> Items;
        protected override IEnumerable<UIDataItem> GetItems()
        {
            return RaceUIDataSource.Items;
        }
    }
}