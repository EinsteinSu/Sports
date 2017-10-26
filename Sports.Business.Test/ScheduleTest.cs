using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sports.Business.ViewModel;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business.Test
{
    [TestClass]
    public class ScheduleTest : TestBase
    {
        [TestMethod]
        public void Add()
        {
            var schedule = Insert();
            CleanSchedule(schedule.Id);
        }

        [TestMethod]
        public void Update()
        {
            var schedule = Insert();
            schedule.Title = "bbb";
            using (var mgr = new ScheduleMgr())
            {
                mgr.Update(schedule);
            }
            var result = new SportDataContext().Schedules.FirstOrDefault(f => f.Id == schedule.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Title, schedule.Title);
            CleanSchedule(schedule.Id);
        }

        protected ScheduleViewModel Insert()
        {
            var teamA = new Team();
            teamA.Name = "a";
            var teamMgr = new TeamMgr();
            teamMgr.Add(teamA);
            var teamB = new Team();
            teamB.Name = "b";
            teamMgr.Add(teamB);
            var scheduleView = new ScheduleViewModel();
            scheduleView.Title = "aaa";
            scheduleView.StartTime = DateTime.Now;
            scheduleView.TeamA = teamA.Id;
            scheduleView.TeamB = teamB.Id;
            var mgr = new ScheduleMgr();
            mgr.Add(scheduleView);
            Assert.IsTrue(scheduleView.Id > 0);
            var result = Context.Schedules.Include(i => i.Teams).FirstOrDefault(f => f.Id == scheduleView.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Teams.Count == 2);
            return scheduleView;
        }

        public void CleanSchedule(int id)
        {
            var item = Context.Schedules.Include(i => i.Teams).FirstOrDefault(f => f.Id == id);
            Assert.IsNotNull(item);
            Context.Schedules.Remove(item);
            foreach (var team in item.Teams.ToList())
            {
                Context.ScheduleTeams.Remove(team);
            }
            
            Context.SaveChanges();
        }
    }
}
