using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IDisplayMgr : ICrudMgr<Display>
    {
    }

    public class DisplayMgr : CrudMgrBase<Display>, IDisplayMgr
    {
        protected override string EntityName => "Display";

        protected override IEnumerable<Display> GetEntries()
        {
            return Context.Displays.ToList();
        }

        protected override Display GetEntry(int id)
        {
            return Context.Displays.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Display item)
        {
            Context.Displays.Add(item);
        }

        protected override void DeleteItem(Display item)
        {
            Context.Displays.Remove(item);
        }
    }
}