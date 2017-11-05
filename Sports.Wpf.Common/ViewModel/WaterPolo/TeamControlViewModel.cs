using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class TeamControlViewModel : TeamData
    {
        public const string ZeroTime = "00:00";
        private string _pauseTime;
        private int _score;
        private int _timeoutCount;

        public override int Score
        {
            get => _score;
            set
            {
                if (value < 0)
                    return;
                SetProperty(ref _score, value, "Score");
            }
        }

        public override string PauseTime
        {
            get => _pauseTime;
            set
            {
                if (value.Equals(ZeroTime))
                    SetProperty(ref _pauseTime, string.Empty, "PauseTime");
                else
                    SetProperty(ref _pauseTime, value, "PauseTime");
            }
        }

        public override int TimeoutCount
        {
            get => _timeoutCount;
            set
            {
                if (value < 0)
                    return;
                SetProperty(ref _timeoutCount, value, "TimeoutCount");
            }
        }
    }
}