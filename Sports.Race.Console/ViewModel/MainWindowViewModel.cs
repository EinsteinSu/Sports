using System.Windows;
using Sports.Wpf.Common.ViewModel;

namespace Sports.Race.Console.ViewModel
{
    public class MainWindowViewModel : MainWindowServiceBase
    {
        protected override Window GetWindow()
        {
            return new MainWindow();
        }
    }
}