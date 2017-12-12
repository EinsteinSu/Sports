using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class RaceRaw
    {
        public int ScheduleId { get; set; }

        public string TotalTime { get; set; }

        public int Court { get; set; }

        public bool IsTimeout { get; set; }

        public int ThirtySeconds { get; set; }

        public TeamRaw TeamA { get; set; }

        public TeamRaw TeamB { get; set; }

        public void FromRace(RaceViewModel data)
        {
            ScheduleId = data.ScheduleId;
            TotalTime = data.TotalTime;
            Court = data.Court;
            IsTimeout = data.IsTimeout;
            ThirtySeconds = data.ThirtySeconds;
            var teamA = new TeamRaw();
            teamA.FromTeamData(data.TeamA);
            TeamA = teamA;
            var teamB = new TeamRaw();
            teamB.FromTeamData(data.TeamB);
            TeamB = teamB;
        }

        public RaceViewModel ToRace()
        {
            var raceViewModel = new RaceViewModel
            {
                ScheduleId = ScheduleId,
                TotalTime = TotalTime,
                Court = Court,
                IsTimeout = IsTimeout,
                ThirtySeconds = ThirtySeconds,
                TeamA = TeamA.ToTeamData(),
                TeamB = TeamB.ToTeamData()
            };
            return raceViewModel;
        }
    }

    public class TeamRaw
    {
        public string TeamName { get; set; }

        public string FlagUrl { get; set; }

        public int TimeoutCount { get; set; }

        public int Score { get; set; }

        public HatColor HatColor { get; set; }

        public string PauseTime { get; set; }

        public PlayerRaw[] Players { get; set; }

        public void FromTeamData(TeamData data)
        {
            TeamName = data.TeamName;
            FlagUrl = data.FlagUrl;
            TimeoutCount = data.TimeoutCount;
            Score = data.Score;
            HatColor = data.HatColor;
            PauseTime = data.PauseTime;
            Players = new PlayerRaw[data.Players.Length];
            for (var i = 0; i < Players.Length; i++)
            {
                var player = new PlayerRaw();
                player.FromPlayerData(data.Players[i]);
                Players[i] = player;
            }
        }

        public TeamData ToTeamData()
        {
            var data = new TeamData
            {
                TeamName = TeamName,
                FlagUrl = FlagUrl,
                TimeoutCount = TimeoutCount,
                Score = Score,
                HatColor = HatColor,
                PauseTime = PauseTime,
                Players = new PlayerData[Players.Length]
            };
            for (var i = 0; i < Players.Length; i++)
                data.Players[i] = Players[i].ToPlayerData();
            return data;
        }
    }

    public class PlayerRaw
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public int Fouls { get; set; }

        public int FoulTime { get; set; }

        public void FromPlayerData(PlayerData data)
        {
            Number = data.Number;
            Name = data.Name;
            Fouls = data.Fouls;
            FoulTime = data.FoulTime;
        }

        public PlayerData ToPlayerData()
        {
            var data = new PlayerData
            {
                Number = Number,
                Name = Name,
                Fouls = Fouls,
                FoulTime = FoulTime
            };
            return data;
        }
    }
}