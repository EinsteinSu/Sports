using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sports.Business.ViewModel;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business.Test
{
    [TestClass]
    public class ScheduleTest : TestBase
    {
        private int _scheduleid;

        [TestCleanup]
        public void CleanUp()
        {
            if (_scheduleid > 0)
                CleanSchedule(_scheduleid);
            Context.Database.ExecuteSqlCommand("Delete from players");
            Context.Database.ExecuteSqlCommand("Delete from teams");
        }

        [TestMethod]
        public void Add()
        {
            var schedule = Insert();
            _scheduleid = schedule.Id;
        }

        [TestMethod]
        public void Update()
        {
            var schedule = Insert();
            _scheduleid = schedule.Id;
            schedule.Title = "bbb";
            using (var mgr = new ScheduleMgr())
            {
                mgr.Update(schedule);
            }
            var result = new SportDataContext().Schedules.FirstOrDefault(f => f.Id == schedule.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Title, schedule.Title);
        }

        [TestMethod]
        public void Update_TeamA()
        {
            var schedule = Insert();
            _scheduleid = schedule.Id;
            //change teama to teamb
            var teamC = new Team();
            teamC.Name = "c";
            new TeamMgr().Add(teamC);
            schedule.TeamA = teamC.Id;
            var mgr = new ScheduleMgr();
            mgr.Update(schedule);
            var result =
                new SportDataContext().ScheduleTeams.FirstOrDefault(
                    w => w.ScheduleId == schedule.Id && w.TeamId == teamC.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            var schedule = Insert();
            _scheduleid = schedule.Id;
            var mgr = new ScheduleMgr();
            mgr.Delete(schedule.Id);
            var result = new SportDataContext().Schedules.Where(w => w.Id == schedule.Id);
            Assert.IsFalse(result.Any());
            var result1 = new SportDataContext().ScheduleTeams.Where(w => w.ScheduleId == schedule.Id);
            Assert.IsFalse(result1.Any());
        }

        [TestMethod]
        public void GetPlayer()
        {
            var schedule = Insert();
            _scheduleid = schedule.Id;
            var playerMgr = new PlayerMgr();
            var player = new Player { Name = "Player A", TeamId = schedule.TeamA };
            playerMgr.Add(player);
            player = new Player { Name = "Player B", TeamId = schedule.TeamB };
            playerMgr.Add(player);
            player = new Player { Name = "Player C", TeamId = schedule.TeamB };
            playerMgr.Add(player);
            var team = Context.ScheduleTeams.FirstOrDefault(f => f.TeamId == schedule.TeamB && f.ScheduleId == schedule.Id);
            Assert.IsNotNull(team);
            // make c to be selected
            var ids = new int[1];
            ids[0] = player.Id;
            var mgr = new ScheduleMgr();
            mgr.SaveScheduleTeamPlayer(_scheduleid, TeamType.Guest, ids);
            var result = mgr.GetPlayer(schedule.Id);
            Assert.AreEqual(1, result.TeamAPlayers.Count());
            Assert.AreEqual(2, result.TeamBPlayers.Count());
            var selectedPlayer = result.TeamBPlayers.FirstOrDefault(f => f.PlayerId == player.Id);
            Assert.IsNotNull(selectedPlayer);
            Assert.IsTrue(selectedPlayer.Selected);
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
            if (item != null)
            {
                Context.Schedules.Remove(item);
                Context.SaveChanges();
            }
        }
    }
}