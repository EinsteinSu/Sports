using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface ISecurityMgr : ICrudMgr<Security> { }
    public class SecurityMgr : CrudMgrBase<Security>, ISecurityMgr
    {
        protected override string EntityName => "Security";
        protected override IEnumerable<Security> GetEntries()
        {
            return Context.Securities.ToList();
        }

        protected override Security GetEntry(int id)
        {
            return Context.Securities.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Security item)
        {
            Context.Securities.Add(item);
        }

        protected override void DeleteItem(Security item)
        {
            Context.Securities.Remove(item);
        }
    }
}
