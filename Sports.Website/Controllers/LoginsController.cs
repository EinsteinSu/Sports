using System.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class LoginsController : CrudControllerBase<Login>
    {
        // GET: Logins
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
        public ActionResult GridViewPartialAddNew(Login item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Login item)
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
            Mgr = new LoginMgr();
        }
    }
}