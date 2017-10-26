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
            foreach (var s in Context.Schedules)
            {
                list.Add(new ScheduleViewModel(s));
            }
            return list;
        }

        public ScheduleViewModel GetItem(int id)
        {
            var schedule = Context.Schedules.FirstOrDefault(f => f.Id == id);
            if (schedule != null)
                return new ScheduleViewModel(schedule);
            return null;
        }

        public void Add(ScheduleViewModel item)
        {
            try
            {
                var schedule = item.ToSchedule();
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
                var schedule = item.ToSchedule();
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
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}