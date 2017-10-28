using System.Collections.Generic;
using DevExpress.Mvvm;

namespace Sports.Common.WaterPolo
{
    public class RaceViewModel : ViewModelBase
    {
        private int _court;
        private bool _isTimeout;
        private string _teamA;
        private string _teamB;
        private string _totalTime;

        public string TotalTime
        {
            get => _totalTime;
            set => SetProperty(ref _totalTime, value, "TotalTie");
        }

        public int Court
        {
            get => _court;
            set => SetProperty(ref _court, value, "Court");
        }

        public bool IsTimeout
        {
            get => _isTimeout;
            set => SetProperty(ref _isTimeout, value, "IsTimeout");
        }

        public string TeamA
        {
            get => _teamA;
            set => SetProperty(ref _teamA, value, "TeamA");
        }

        public string TeamB
        {
            get => _teamB;
            set => SetProperty(ref _teamB, value, "TeamB");
        }
    }

    public class TeamData : ViewModelBase
    {
        private HatColor _hatColor;

        private string _pauseTime;

        private IList<PlayerData> _players;
        private int _score;
        private string _teamName;
        private int _timeoutCount;

        public string TeamName
        {
            get => _teamName;
            set => SetProperty(ref _teamName, value, "TeamName");
        }

        //todo reference as darshboard
        public string FlagUrl { get; set; }

        public int TimeoutCount
        {
            get => _timeoutCount;
            set => SetProperty(ref _timeoutCount, value, "TimeoutCount");
        }

        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value, "Score");
        }

        public HatColor HatColor
        {
            get => _hatColor;
            set => SetProperty(ref _hatColor, value, "HatColor");
        }

        public IList<PlayerData> Players
        {
            get => _players;
            set => SetProperty(ref _players, value, "Players");
        }

        public string PauseTime
        {
            get => _pauseTime;
            set => SetProperty(ref _pauseTime, value, "PauseTime");
        }
    }

    public class PlayerData : ViewModelBase
    {
        private int _fouls;

        private int _foulTime;

        private string _name;
        private int _number;

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value, "Number");
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, "Name");
        }

        public int Fouls
        {
            get => _fouls;
            set => SetProperty(ref _fouls, value, "Fouls");
        }

        public int FoulTime
        {
            get => _foulTime;
            set => SetProperty(ref _foulTime, value, "FoulTime");
        }
    }

    public enum HatColor
    {
        White,
        Blue
    }
}