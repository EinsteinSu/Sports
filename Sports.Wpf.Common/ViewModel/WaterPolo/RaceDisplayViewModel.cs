using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class RaceDisplayViewModel : RaceViewModel
    {
        public string CourtToString
        {
            get
            {
                switch (Court)
                {
                    case 1:
                        return "1st";
                    case 2:
                        return "2nd";
                    case 3:
                        return "3rd";
                    case 4:
                        return "4th";
                }
                return Court > 4 ? $"Extral({Court})" : string.Empty;
            }
        }
    }
}