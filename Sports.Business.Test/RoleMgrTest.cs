using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sports.DataAccess.Models;

namespace Sports.Business.Test
{
    [TestClass]
    public class RoleMgrTest : TestBase
    {
        [TestCleanup]
        public void CleanUp()
        {
            Context.Database.ExecuteSqlCommand("Delete from Roles");
            Context.Database.ExecuteSqlCommand("Delete from Securities");
            Context.Database.ExecuteSqlCommand("Delete from SecurityGroups");
        }

        [TestMethod]
        public void GetRoleSecurityEditViewModel()
        {
            var role = new Role { Name = "A test" };
            var group = new SecurityGroup
            {
                Name = "Group A",
                Securities = new List<Security>
            {
                new Security {Name = "Security A"},
                new Security {Name = "Security B"}
            }
            };
            var securityGroupMgr = new SecurityGroupMgr();
            securityGroupMgr.Add(group);
            var grantedSecurities = new int[1];
            Assert.IsTrue(group.Securities.Any());
            grantedSecurities[0] = group.Securities.First().Id;

            var mgr = new RoleMgr();
            mgr.Add(role);
            mgr.UpdateSecurities(role.Id, grantedSecurities);

            var result = mgr.GetRoleSecurityEditViewModel(role.Id);
            var selectedSecurity = result.SecurityGroups[0].Securities
                .FirstOrDefault(f => f.SecurityId == group.Securities.First().Id);
            Assert.IsNotNull(selectedSecurity);
            Assert.IsTrue(selectedSecurity.Selected);
        }
    }
}
