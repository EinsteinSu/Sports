using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class FlagsController : CrudControllerBase<Flag>
    {
        // GET: Flags
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return GetList();
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Flag item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Flag item)
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
            Mgr = new FlagMgr();
        }
    }
}