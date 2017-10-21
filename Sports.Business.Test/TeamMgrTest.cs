using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business.Test
{
    [TestClass]
    public class TeamMgrTest : TestBase
    {
        private readonly TeamMgr mgr = new TeamMgr();

        [TestMethod]
        public void Add_Test()
        {
            var team = Insert();
            CleanTeam(team.Id);
        }



        [TestMethod]
        public void Update_Test()
        {
            const string modifying = "Test";
            var team = Insert();
            team.Name = modifying;
            mgr.Update(team);
            var team1 = mgr.GetItem(team.Id);
            Assert.AreEqual(team1.Name, modifying);
            CleanTeam(team1.Id);
        }

        [TestMethod]
        public void Delete_Test()
        {
            var team = Insert();
            mgr.Delete(team.Id);
            var team1 = mgr.GetItem(team.Id);
            Assert.IsTrue(team1.Deleted);
            CleanTeam(team1.Id);
        }

        private void CleanTeam(int id)
        {
            var team = Context.Teams.FirstOrDefault(f => f.Id == id);
            if (team != null)
            {
                Context.Teams.Remove(team);
                Context.SaveChanges();
            }
        }

        private Team Insert()
        {
            var team = new Team
            {
                Name = "China",
                Code = "CHN",
                ShortName = "CHN",
                Description = "This is a test team"
            };
            mgr.Add(team);
            Assert.IsTrue(team.Id > 0);
            return team;
        }
    }
}
