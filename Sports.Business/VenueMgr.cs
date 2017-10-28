using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IVenueMgr : ICrudMgr<Venue>
    {
        bool IsStarted(int id);
    }

    public class VenueMgr : CrudMgrBase<Venue>, IVenueMgr
    {
        protected override string EntityName => "Venue";

        protected override IEnumerable<Venue> GetEntries()
        {
            return Context.Venues.ToList();
        }

        protected override Venue GetEntry(int id)
        {
            return Context.Venues.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Venue item)
        {
            Context.Venues.Add(item);
        }

        protected override void DeleteItem(Venue item)
        {
            Context.Venues.Remove(item);
        }

        public bool IsStarted(int id)
        {
            var item = GetItem(id);
            return item.State == VenueState.Racing;
        }
    }
}