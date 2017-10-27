using System.Web.Mvc;
using DevExpress.Web;
using Sports.Business;
using Sports.Business.ViewModel;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class SchedulesController : CrudControllerBase<ScheduleViewModel>
    {
        // GET: Schedules
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult EditModesPartial()
        {
            return GetList();
        }
        public ActionResult ChangeEditModePartial(GridViewEditingMode editMode)
        {
            GridViewEditingDemosHelper.EditMode = editMode;
            return GetList();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return GetList();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(ScheduleViewModel item)
        {
            return AddNew(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(ScheduleViewModel item)
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
            Mgr = new ScheduleMgr();
        }
    }
}