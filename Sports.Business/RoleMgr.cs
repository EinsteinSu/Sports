using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IRoleMgr : ICrudMgr<Role> { }

    public class RoleMgr:CrudMgrBase<Role>,IRoleMgr
    {
        protected override string EntityName => "Role";
        protected override IEnumerable<Role> GetEntries()
        {
            return Context.Roles.ToList();
        }

        protected override Role GetEntry(int id)
        {
            return Context.Roles.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Role item)
        {
            Context.Roles.Add(item);
        }

        protected override void DeleteItem(Role item)
        {
            Context.Roles.Remove(item);
        }
    }
}
