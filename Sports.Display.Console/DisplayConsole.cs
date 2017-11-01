using DevExpress.Mvvm;
using Sports.Wpf.Common.ViewModel.WaterPolo;

namespace Sports.Display.Console
{
    public class DisplayConsole : ViewModelBase
    {
        private RaceDisplayViewModel _race;
        public DisplayConsoleSetting Settings { get; set; }

        public RaceDisplayViewModel Race
        {
            get => _race;
            set => SetProperty(ref _race, value, "Race");
        }
    }
}