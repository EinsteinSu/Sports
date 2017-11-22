using DevExpress.Mvvm;

namespace Sports.Common.WaterPolo
{
    public class RaceViewModel : ViewModelBase
    {
        private int _court;
        private bool _isTimeout;
        private TeamData _teamA;
        private TeamData _teamB;
        private int _thirtySeconds;
        private string _totalTime;

        public RaceViewModel()
        {
            _thirtySeconds = 30;
        }

        public int ScheduleId { get; set; }

        public virtual string TotalTime
        {
            get => _totalTime;
            set => SetProperty(ref _totalTime, value, "TotalTime");
        }

        public virtual int Court
        {
            get => _court;
            set => SetProperty(ref _court, value, "Court");
        }

        public bool IsTimeout
        {
            get => _isTimeout;
            set => SetProperty(ref _isTimeout, value, "IsTimeout");
        }

        public int ThirtySeconds
        {
            get => _thirtySeconds;
            set => SetProperty(ref _thirtySeconds, value, "ThirtySeconds");
        }

        public TeamData TeamA
        {
            get => _teamA;
            set => SetProperty(ref _teamA, value, "TeamA");
        }

        public TeamData TeamB
        {
            get => _teamB;
            set => SetProperty(ref _teamB, value, "TeamB");
        }
    }

    public enum HatColor
    {
        White,
        Blue
    }
}