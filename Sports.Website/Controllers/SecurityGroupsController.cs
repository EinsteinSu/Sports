using System.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class SecurityGroupsController : CrudControllerBase<SecurityGroup>
    {
        // GET: SecurityGroups
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
        public ActionResult GridViewPartialAddNew(SecurityGroup item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(SecurityGroup item)
        {
            return Update(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialDelete(int id)
        {
            return Delete(id);
        }

        protected override void InitializeInterfaces()
        {
            Mgr = new SecurityGroupMgr();
        }
    }
}