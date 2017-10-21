using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IHardwareMgr : ICrudMgr<Hardware>
    {

    }

    public class HardwareMgr : CrudMgrBase<Hardware>, IHardwareMgr
    {
        protected override string EntityName { get { return "Hardware"; } }
        protected override IEnumerable<Hardware> GetEntries()
        {
            return Context.Hardwares.ToList();
        }

        protected override Hardware GetEntry(int id)
        {
            return Context.Hardwares.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Hardware item)
        {
            Context.Hardwares.Add(item);
        }

        protected override void DeleteItem(Hardware item)
        {
            Context.Hardwares.Remove(item);
        }
    }
}
