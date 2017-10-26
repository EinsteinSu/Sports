using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business.ViewModel
{
    public class ScheduleViewModel : Schedule
    {
        private int _teamA;
        private int _teamAScore;
        private int _teamB;
        private int _teamBScore;

        public ScheduleViewModel() { }

        public ScheduleViewModel(Schedule schedule)
        {
            Id = schedule.Id;
            Title = schedule.Title;
            StartTime = schedule.StartTime;
            EndTime = schedule.EndTime;
            Teams = schedule.Teams;
            Venue = schedule.Venue;
            VenueId = schedule.VenueId;
        }

        public int TeamA
        {
            get => _teamA;
            set
            {
                _teamA = value;
                SetTeam(value, TeamType.Host);
            }
        }

        public int TeamAScore
        {
            get => _teamAScore;
            set
            {
                _teamAScore = value;
                SetTeamScore(TeamA, value);
            }
        }

        public int TeamB
        {
            get => _teamB;
            set
            {
                _teamB = value;
                SetTeam(value, TeamType.Guest);
            }
        }

        public int TeamBScore
        {
            get => _teamBScore;
            set
            {
                _teamBScore = value;
                SetTeamScore(TeamB, value);
            }
        }

        protected void SetTeam(int teamId, TeamType type)
        {
            if (Teams == null)
                Teams = new List<ScheduleTeam>();
            if (TeamCanBeCreate(teamId))
                Teams.Add(new ScheduleTeam { TeamId = teamId, TeamType = type });
        }

        private bool TeamCanBeCreate(int teamId)
        {
            return Teams.All(f => f.Id != teamId) && Teams.Count < 2;
        }

        public void SetTeamScore(int teamId, int score)
        {
            var team = Teams.FirstOrDefault(f => f.Id == teamId);
            if (team != null)
                team.Score = score;
        }

        public Schedule ToSchedule()
        {
            var schedule = new Schedule();
            schedule.Id = Id;
            schedule.Title = Title;
            schedule.StartTime = StartTime;
            schedule.EndTime = EndTime;
            schedule.Venue = Venue;
            schedule.Teams = Teams;
            schedule.VenueId = VenueId;
            return schedule;
        }
    }
}