using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;
using Sports.Business.ViewModel;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IScheduleMgr : ICrudMgr<ScheduleViewModel>, IDisposable
    {
        SchedulePlayerEditViewModel GetPlayer(int scheduleId);

        void SaveScheduleTeamPlayer(int scheduleId, TeamType type,int[] players);
    }

    public class ScheduleMgr : IScheduleMgr
    {
        protected readonly SportDataContext Context = new SportDataContext();
        public IEnumerable<ScheduleViewModel> GetItems()
        {
            var list = new List<ScheduleViewModel>();
            foreach (var s in Context.Schedules.Include(i => i.Teams))
            {
                list.Add(new ScheduleViewModel(s));
            }
            return list;
        }

        public ScheduleViewModel GetItem(int id)
        {
            var schedule = Context.Schedules.Include(i => i.Teams).FirstOrDefault(f => f.Id == id);
            if (schedule != null)
                return new ScheduleViewModel(schedule);
            return null;
        }

        private void Reprocess(ScheduleViewModel viewModel, Schedule data)
        {
            if (data.Teams == null)
            {
                data.Teams = new List<ScheduleTeam>();
            }
            var canAdd = Context.ScheduleTeams.Count(c => c.ScheduleId == data.Id) < 2;
            ReprocessTeam(viewModel.TeamA, TeamType.Host, data, canAdd);
            ReprocessTeam(viewModel.TeamB, TeamType.Guest, data, canAdd);
        }

        private void ReprocessTeam(int teamId, TeamType type, Schedule data, bool canAdd)
        {
            var team = Context.ScheduleTeams.FirstOrDefault(f => f.TeamType == type && f.ScheduleId == data.Id);

            if (team == null && canAdd)
            {
                data.Teams.Add(new ScheduleTeam { TeamId = teamId, TeamType = type });
            }
            else
            {
                if (team != null)
                {
                    team.Score = team.Score;
                    team.TeamId = teamId;
                    team.ScheduleId = data.Id;
                    Context.Entry(team).State = EntityState.Modified;
                }
            }
        }

        public void Add(ScheduleViewModel item)
        {
            try
            {
                var schedule = item.ToSchedule(Reprocess);
                Context.Schedules.Add(schedule);
                Context.SaveChanges();
                item.Id = schedule.Id;
            }
            catch (Exception e)
            {
                throw new Exception("Could not add schedule! " + e.Message);
            }
        }

        public void Update(ScheduleViewModel item)
        {
            try
            {
                var schedule = item.ToSchedule(Reprocess);
                Context.Entry(schedule).State = EntityState.Modified;
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Could not update schedule! " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var item = Context.Schedules.FirstOrDefault(f => f.Id == id);
                if (item == null)
                {
                    throw new KeyNotFoundException(string.Format("Could not found id {0} in {1}", id, "Schedule"));
                }
                Context.Schedules.Remove(item);
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("delete {0} failed, {1}", "Schedule", exception.Message));
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public SchedulePlayerEditViewModel GetPlayer(int scheduleId)
        {
            var schedulePlayer = new SchedulePlayerEditViewModel();
            var schedule = GetItem(scheduleId);
            schedulePlayer.ScheduleId = scheduleId;
            schedulePlayer.Title = schedule.Title;
            ProcessSchedulePlayer(TeamType.Host, scheduleId, schedulePlayer);
            ProcessSchedulePlayer(TeamType.Guest, scheduleId, schedulePlayer);
            return schedulePlayer;
        }

        public void SaveScheduleTeamPlayer(int scheduleId, TeamType type, int[] players)
        {
            var team = Context.ScheduleTeams.FirstOrDefault(f => f.TeamType == type && f.ScheduleId == scheduleId);
            if (team != null && players != null)
            {
                team.Players = JsonConvert.SerializeObject(players);
                Context.Entry(team).Property(p => p.Players).IsModified = true;
                Context.SaveChanges();
            }
            else
            {
                throw new Exception("Could not found any teams!");
            }
        }

        private void ProcessSchedulePlayer(TeamType type, int scheduleId, SchedulePlayerEditViewModel schedulePlayer)
        {
            var team = Context.ScheduleTeams.Include(i => i.Team).FirstOrDefault(f => f.TeamType == type && f.ScheduleId == scheduleId);
            if (team?.TeamId != null)
            {
                var list = new List<ScheduleTeamPlayerViewModel>();
                int[] selectedPlayers = !string.IsNullOrEmpty(team.Players) ? JsonConvert.DeserializeObject<int[]>(team.Players) : new int[0];
                foreach (var player in Context.Players.Where(w => w.TeamId == team.TeamId.Value))
                {
                    var playerViewModel = new ScheduleTeamPlayerViewModel { Name = player.Name, PlayerId = player.Id };
                    list.Add(playerViewModel);
                    playerViewModel.Selected = selectedPlayers.Any(f => f == player.Id);
                }
                if (type == TeamType.Host)
                {
                    schedulePlayer.TeamAId = team.TeamId.Value;
                    schedulePlayer.TeamAName = team.Team.Name;
                    schedulePlayer.TeamAPlayers = list;
                }
                else
                {
                    schedulePlayer.TeamBId = team.TeamId.Value;
                    schedulePlayer.TeamBName = team.Team.Name;
                    schedulePlayer.TeamBPlayers = list;
                }
            }
        }
    }
}