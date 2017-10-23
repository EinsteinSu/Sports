using System.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class SecuritiesController : CrudControllerBase<Security>
    {
        // GET: Securities
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
        public ActionResult GridViewPartialAddNew(Security item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Security item)
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
            Mgr = new SecurityMgr();
        }
    }
}