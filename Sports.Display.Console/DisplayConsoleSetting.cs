using Sports.Wpf.Common.ViewModel.WaterPolo;

namespace Sports.Display.Console
{
    public class DisplayConsoleSetting
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int ListeningPort { get; set; }
    }

    public class DisplayConsole
    {
        public DisplayConsoleSetting Settings { get; set; }

        public RaceDisplayViewModel Race { get; set; }
    }
}