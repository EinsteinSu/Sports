using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sports.Business;
using Sports.DataAccess.Models;
using Sports.Website.Commons;

namespace Sports.Website.Controllers
{
    public class HardwaresController : CrudControllerBase<Hardware>
    {
        // GET: Hardwares
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return GetList();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Sports.DataAccess.Models.Hardware item)
        {
            return AddNew(item);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Sports.DataAccess.Models.Hardware item)
        {
            return Update(item);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(int id)
        {
            return Delete(id);
        }

        protected override void InitializeInterfaces()
        {
            Mgr = new HardwareMgr();
        }
    }
}