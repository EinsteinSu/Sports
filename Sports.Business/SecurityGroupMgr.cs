using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface ISecurityGroupMgr : ICrudMgr<SecurityGroup> { }

    public class SecurityGroupMgr : CrudMgrBase<SecurityGroup>, ISecurityGroupMgr
    {
        protected override string EntityName => "Security Group";
        protected override IEnumerable<SecurityGroup> GetEntries()
        {
            return Context.SecurityGroups.ToList();
        }

        protected override SecurityGroup GetEntry(int id)
        {
            return Context.SecurityGroups.Include(i => i.Securities).FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(SecurityGroup item)
        {
            Context.SecurityGroups.Add(item);
        }

        protected override void DeleteItem(SecurityGroup item)
        {
            Context.SecurityGroups.Remove(item);
        }
    }
}
