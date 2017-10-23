using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using Sports.Wpf.Common.DataModel;

namespace Sports.Wpf.Common.ViewModel
{
    //A View Model for a GroupedItemsPage
    public abstract class GroupedItemsViewModelBase : ViewModelBase, INavigationAware
    {
        IEnumerable<UIDataItem> items;
        public GroupedItemsViewModelBase() { }

        protected abstract IEnumerable<UIDataItem> GetItems();

        public IEnumerable<UIDataItem> Items
        {
            get { return items; }
            private set { SetProperty<IEnumerable<UIDataItem>>(ref items, value, "Items"); }
        }
        public void LoadState(object navigationParameter)
        {
            Items = GetItems();
        }
        #region INavigationAware Members
        public void NavigatedFrom(NavigationEventArgs e)
        {
        }
        public void NavigatedTo(NavigationEventArgs e)
        {
            LoadState(e.Parameter);
        }
        public void NavigatingFrom(NavigatingEventArgs e)
        {
        }
        #endregion
    }
}
