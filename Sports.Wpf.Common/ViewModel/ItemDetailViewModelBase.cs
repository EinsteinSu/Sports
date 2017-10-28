using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using Sports.Wpf.Common.DataModel;

namespace Sports.Wpf.Common.ViewModel
{
    //A View Model for an ItemDetailPage
    public abstract class ItemDetailViewModelBase : ViewModelBase, INavigationAware
    {
        private UIDataItem _selectedItem;
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value, "Title");
        }

        public UIDataItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, "SelectedItem");
        }

        protected abstract UIDataItem GetItem(int id);

        private void LoadState(object navigationParameter)
        {
            var id = int.Parse(navigationParameter.ToString());
            var item = GetItem(id);
            Title = item.Title;
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