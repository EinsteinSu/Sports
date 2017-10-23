using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sports.Business;
using Sports.DataAccess.Models;

namespace Sports.Website.Commons
{
    public static class DataProvider
    {
        public static IEnumerable<Team> GetTeams()
        {
            return new TeamMgr().GetItems();
        }

        public static IEnumerable<SecurityGroup> GetSecurityGroups()
        {
            return new SecurityGroupMgr().GetItems();
        }

        public static IEnumerable<Security> GetSecurities()
        {
            return new SecurityMgr().GetItems();
        }

        public static IEnumerable<Role> GetRoles()
        {
            return new RoleMgr().GetItems();
        }
    }
}