using System;
using System.Collections.Generic;
using System.Linq;
using Sports.Common;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface ILoginMgr : ICrudMgr<Login>
    {
        Login Login(string userName, string password);
    }

    public class LoginMgr : CrudMgrBase<Login>, ILoginMgr
    {
        protected override string EntityName => "Login";

        protected override IEnumerable<Login> GetEntries()
        {
            var list = Context.Logins.ToList();
            foreach (var item in list)
            {
                item.Password = item.Password.Decrypt();
            }
            return list;
        }

        protected override Login GetEntry(int id)
        {
           return Context.Logins.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Login item)
        {
            item.Password = item.Password.Encrypt();
            Context.Logins.Add(item);
        }

        protected override void UpdateItem(Login item)
        {
            item.Password = item.Password.Encrypt();
            base.UpdateItem(item);
        }

        protected override void DeleteItem(Login item)
        {
            Context.Logins.Remove(item);
        }

        public Login Login(string userName, string password)
        {
            var pwd = password.Decrypt();
            return Context.Logins.FirstOrDefault(f =>
                f.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && f.Password.Equals(pwd));
        }
    }
}