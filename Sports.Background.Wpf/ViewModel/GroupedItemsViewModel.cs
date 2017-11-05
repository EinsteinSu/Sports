using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using Sports.Background.Wpf.DataModel;
using Sports.Wpf.Common.DataModel;
using Sports.Wpf.Common.ViewModel;

namespace Sports.Background.Wpf.ViewModel
{
    //A View Model for a GroupedItemsPage
    public class GroupedItemsViewModel : GroupedItemsViewModelBase
    {
        protected override IEnumerable<UIDataItem> GetItems()
        {
            return null;
        }
    }
}
