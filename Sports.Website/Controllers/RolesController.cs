using System.Collections.Generic;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Sports.Business;
using Sports.Business.ViewModel;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class RolesController : CrudControllerBase<Role>
    {
        private readonly ISecurityGroupMgr _securityGroupMgr;
        public RolesController()
        {
            _securityGroupMgr = new SecurityGroupMgr();
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return GetList();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Role item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Role item)
        {
            return Update(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialDelete(int id)
        {
            return Delete(id);
        }

        public ActionResult RoleSecurityEdit(int id)
        {
            ViewBag.Id = id;
            return View(((IRoleMgr)Mgr).GetRoleSecurityEditViewModel(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RoleSecurityEdit([ModelBinder(typeof(DevExpressEditorsBinder))] RoleSecurityEditViewModel model)
        {
            var mgr = Mgr as IRoleMgr;
            var list = new List<int>();
            foreach (var group in _securityGroupMgr.GetItems())
            {
                var groupSecurities = CheckBoxListExtension.GetSelectedValues<int>(group.Name);
                list.AddRange(groupSecurities);
            }
            mgr?.UpdateSecurities(model.RoleId, list.ToArray());
            return View("Index");
        }

        protected override void InitializeInterfaces()
        {
            Mgr = new RoleMgr();
        }
    }
}