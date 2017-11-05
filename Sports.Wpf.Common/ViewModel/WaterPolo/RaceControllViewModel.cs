using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class RaceControllViewModel : RaceViewModel
    {
        private int _court;
        private string _totalTime;

        public override string TotalTime
        {
            get => _totalTime;
            set
            {
                if (!value.Equals(_totalTime))
                {
                    if (TeamA != null)
                        DecreasePlayerFoulTime(TeamA);
                    if (TeamB != null)
                        DecreasePlayerFoulTime(TeamB);
                }
                SetProperty(ref _totalTime, value, "TotalTime");
            }
        }

        public override int Court
        {
            get => _court;
            set
            {
                if (value < 1)
                    return;
                SetProperty(ref _court, value, "Court");
            }
        }

        protected virtual void DecreasePlayerFoulTime(TeamData team)
        {
            foreach (var player in team.Players)
            {
                if (player.FoulTime > 0)
                {
                    player.FoulTime--;
                }
            }
        }
    }
}