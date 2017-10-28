using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface ICentralMgr : ICrudMgr<Central>
    {
    }

    public class CentralMgr : CrudMgrBase<Central>, ICentralMgr
    {
        protected override string EntityName => "Central";

        protected override void AddItem(Central item)
        {
            Context.Centrals.Add(item);
        }

        protected override void DeleteItem(Central item)
        {
            Context.Centrals.Remove(item);
        }

        protected override IEnumerable<Central> GetEntries()
        {
            return Context.Centrals.ToList();
        }

        protected override Central GetEntry(int id)
        {
            return Context.Centrals.FirstOrDefault(f => f.Id == id);
        }
    }
}