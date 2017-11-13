using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Sports.Business;
using Sports.Business.ViewModel;
using Sports.DataAccess.Models;
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

        public ActionResult SchedulePlayerEdit(int scheduleId)
        {
            ViewBag.ScheduleId = scheduleId;
            return View(((IScheduleMgr)Mgr).GetPlayer(scheduleId));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SchedulePlayerEdit([ModelBinder(typeof(DevExpressEditorsBinder))] SchedulePlayerEditViewModel model)
        {
            var mgr = Mgr as IScheduleMgr;
            var teamA = CheckBoxListExtension.GetSelectedValues<int>("TeamAPlayers");
            var teamB = CheckBoxListExtension.GetSelectedValues<int>("TeamBPlayers");
            if (mgr != null)
            {
                mgr.SaveScheduleTeamPlayer(model.ScheduleId, TeamType.Host, teamA);
                mgr.SaveScheduleTeamPlayer(model.ScheduleId, TeamType.Guest, teamB);
            }
            return View("Index");
        }

        public ActionResult PlayersGridViewPartial(int scheduleId)
        {
            ViewBag.ScheduleId = scheduleId;
            return PartialView("_PlayersGridViewPartial", ((IScheduleMgr) Mgr).GetPlayer(scheduleId));
        }

        protected override void InitializeInterfaces()
        {
            Mgr = new ScheduleMgr();
        }
    }
}