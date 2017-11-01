using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class RaceControllViewModel : RaceViewModel
    {
        private string _totalTime;

        public override string TotalTime
        {
            get => _totalTime;
            set
            {
                SetProperty(ref _totalTime, value, "TotalTime");
                //todo check wheter the value equals previous value, refer to Inotification 
                DecreasePlayerFoulTime(TeamA);
                DecreasePlayerFoulTime(TeamB);
            }
        }

        protected virtual void DecreasePlayerFoulTime(TeamData team)
        {
            foreach (var player in team.Players)
            {
                player.FoulTime--;
            }

        }
    }
}