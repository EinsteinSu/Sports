using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface ITeamMgr : ICrudMgr<Team>
    {

    }

    public class TeamMgr : CrudMgrBase<Team>, ITeamMgr
    {
        protected override string EntityName { get { return "Team"; } }
        protected override IEnumerable<Team> GetEntries()
        {
            return Context.Teams.Where(w => !w.Deleted).OrderBy(o => o.Code).ToList();
        }

        protected override Team GetEntry(int id)
        {
            return Context.Teams.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Team item)
        {
            Context.Teams.Add(item);
        }

        protected override void DeleteItem(Team item)
        {
            item.Deleted = true;
            Context.Entry(item).State = EntityState.Modified;
        }
    }
}