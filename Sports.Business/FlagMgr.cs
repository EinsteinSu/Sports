using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IFlagMgr : ICrudMgr<Flag>
    {

    }

    public class FlagMgr : CrudMgrBase<Flag>, IFlagMgr
    {
        protected override string EntityName { get { return "Flag"; } }

        protected override IEnumerable<Flag> GetEntries()
        {
            return Context.Flags.ToList();
        }

        protected override Flag GetEntry(int id)
        {
            return Context.Flags.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Flag item)
        {
            Context.Flags.Add(item);
        }

        protected override void DeleteItem(Flag item)
        {
            Context.Flags.Remove(item);
        }
    }
}
