using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sports.Business.ViewModel;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IRoleMgr : ICrudMgr<Role>
    {
        RoleSecurityEditViewModel GetRoleSecurityEditViewModel(int roleId);

        void UpdateSecurities(int roleId, int[] securities);
    }

    public class RoleMgr : CrudMgrBase<Role>, IRoleMgr
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

        public RoleSecurityEditViewModel GetRoleSecurityEditViewModel(int roleId)
        {
            var role = GetItem(roleId);
            var roleSecurity = new RoleSecurityEditViewModel
            {
                RoleId = roleId,
                Name = role.Name
            };
            var grantedSecurities = !string.IsNullOrEmpty(role.Securities)
                ? JsonConvert.DeserializeObject<int[]>(role.Securities)
                : new int[0];
            var group = Context.SecurityGroups.Include(i => i.Securities).ToList();
            foreach (var securityGroup in group)
            {
                var gv = new RoleSecurityGroupViewModel { GroupId = securityGroup.Id, GroupName = securityGroup.Name };
                foreach (var security in securityGroup.Securities)
                {
                    var sv = new SecurityViewModel
                    {
                        SecurityId = security.Id,
                        Name = security.Name
                    };
                    sv.Selected = grantedSecurities.Any(a => a == sv.SecurityId);
                    gv.Securities.Add(sv);
                }
                roleSecurity.SecurityGroups.Add(gv);
            }
            return roleSecurity;
        }

        public void UpdateSecurities(int roleId, int[] securities)
        {
            var role = GetItem(roleId);
            role.Securities = JsonConvert.SerializeObject(securities);
            Context.Entry(role).Property(p => p.Securities).IsModified = true;
            Context.SaveChanges();
        }
    }
}
