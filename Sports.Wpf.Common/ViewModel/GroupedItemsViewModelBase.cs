using System.Collections.Generic;
using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using Sports.Wpf.Common.DataModel;

namespace Sports.Wpf.Common.ViewModel
{
    //A View Model for a GroupedItemsPage
    public abstract class GroupedItemsViewModelBase : ViewModelBase, INavigationAware
    {
        private IEnumerable<UIDataItem> _items;

        public IEnumerable<UIDataItem> Items
        {
            get => _items;
            private set => SetProperty(ref _items, value, "Items");
        }

        protected abstract IEnumerable<UIDataItem> GetItems();

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