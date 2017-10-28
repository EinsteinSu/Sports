using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sports.Business.ViewModel;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IScheduleMgr : ICrudMgr<ScheduleViewModel>, IDisposable
    {
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
    }
}