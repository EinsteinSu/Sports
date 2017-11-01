using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class PlayerDisplayViewModel : PlayerData
    {
        public const string Cycle = "●";
        public string Foul1 => Fouls > 0 ? Cycle : string.Empty;
        public string Foul2 => Fouls > 1 ? Cycle : string.Empty;
        public string Foul3 => Fouls > 2 ? Cycle : string.Empty;

        public string FoulTimeForDisplay
        {
            get
            {
                if (FoulTime == 0)
                    return string.Empty;
                return FoulTime.ToString().PadLeft(2, '0');
            }
        }
    }
}