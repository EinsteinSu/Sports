using System;
using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business.ViewModel
{
    public class ScheduleViewModel : Schedule
    {
        public ScheduleViewModel()
        {
        }

        public ScheduleViewModel(Schedule schedule)
        {
            Id = schedule.Id;
            Title = schedule.Title;
            StartTime = schedule.StartTime;
            EndTime = schedule.EndTime;
            Teams = schedule.Teams;
            TeamA = GetTeam(TeamType.Host);
            TeamB = GetTeam(TeamType.Guest);
            TeamAScore = GetTeamScore(TeamType.Host);
            TeamBScore = GetTeamScore(TeamType.Guest);
            Venue = schedule.Venue;
            VenueId = schedule.VenueId;
        }

        public int TeamA { get; set; }


        public int TeamAScore { get; set; }

        public int TeamB { get; set; }

        public int TeamBScore { get; set; }

        public int GetTeam(TeamType type)
        {
            var team = Teams?.FirstOrDefault(f => f.TeamType == type);
            if (team != null)
                return team.Id;
            return 0;
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

        public int GetTeamScore(TeamType type)
        {
            var team = Teams?.FirstOrDefault(f => f.TeamType == type);
            if (team != null)
                return team.Score;
            return 0;
        }

        public void SetTeamScore(int teamId, int score)
        {
            var team = Teams.FirstOrDefault(f => f.Id == teamId);
            if (team != null)
                team.Score = score;
        }

        public Schedule ToSchedule(Action<ScheduleViewModel, Schedule> reProcess = null)
        {
            var schedule = new Schedule
            {
                Id = Id,
                Title = Title,
                StartTime = StartTime,
                EndTime = EndTime,
                VenueId = VenueId,
            };
            reProcess?.Invoke(this, schedule);
            return schedule;
        }
    }
}