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
    }
}