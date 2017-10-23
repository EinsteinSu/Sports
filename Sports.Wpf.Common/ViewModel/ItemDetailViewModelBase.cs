using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using Sports.Wpf.Common.DataModel;

namespace Sports.Wpf.Common.ViewModel
{
    //A View Model for an ItemDetailPage
    public abstract class ItemDetailViewModelBase : ViewModelBase, INavigationAware
    {
        UIDataItem _selectedItem;

        protected abstract UIDataItem GetItem(string id);
        public ItemDetailViewModelBase() { }
        public UIDataItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty<UIDataItem>(ref _selectedItem, value, "SelectedItem"); }
        }
        private void LoadState(object navigationParameter)
        {
            var id = (string)navigationParameter;
            UIDataItem item = GetItem(id);
            SelectedItem = item;
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
