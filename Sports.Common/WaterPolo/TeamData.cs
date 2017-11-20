using System;
using System.Collections.Generic;
using DevExpress.Mvvm;

namespace Sports.Common.WaterPolo
{
    public class TeamData : ViewModelBase
    {
        private HatColor _hatColor;

        private string _pauseTime;

        private PlayerData[] _players;
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

        public virtual int TimeoutCount
        {
            get => _timeoutCount;
            set => SetProperty(ref _timeoutCount, value, "TimeoutCount");
        }

        public virtual int Score
        {
            get => _score;
            set => SetProperty(ref _score, value, "Score");
        }

        public HatColor HatColor
        {
            get => _hatColor;
            set => SetProperty(ref _hatColor, value, "HatColor");
        }

        public PlayerData[] Players
        {
            get => _players;
            set => SetProperty(ref _players, value, "Players");
        }

        public virtual string PauseTime
        {
            get => _pauseTime;
            set => SetProperty(ref _pauseTime, value, "PauseTime");
        }

        public void DecreaseFoulTimes()
        {
            foreach (var player in Players)
            {
                player.DecreaseFoulTime();
            }
        }
    }
}