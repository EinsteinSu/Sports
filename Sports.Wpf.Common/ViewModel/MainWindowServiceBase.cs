using System.Windows;
using DevExpress.Mvvm.UI;
using Sports.Wpf.Common.ViewModel.Interfaces;

namespace Sports.Wpf.Common.ViewModel
{
    public abstract class MainWindowServiceBase : ServiceBase, IMainWindowService
    {
        public void ShowMainWindow()
        {
            var window = GetWindow();
            window.Show();
        }

        protected abstract Window GetWindow();
    }
}