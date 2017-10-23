using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class VenuesController : CrudControllerBase<Venue>
    {
        // GET: Venues
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
        public ActionResult GridViewPartialAddNew(Venue item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Venue item)
        {
            return Update(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialDelete(int id)
        {
            return Delete(id);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }

        protected override void InitializeInterfaces()
        {
            Mgr = new VenueMgr();
        }
    }
}