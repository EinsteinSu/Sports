using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sports.Business;

namespace Sports.Website.Commons
{
    public abstract class CrudControllerBase<T> : Controller
    {
        protected ICrudMgr<T> Mgr;

        public CrudControllerBase(ICrudMgr<T> mgr)
        {
            Mgr = mgr;
        }

        public CrudControllerBase()
        {
            InitializeInterfaces();
        }

        protected abstract void InitializeInterfaces();

        protected virtual ActionResult GetList(string viewName = "_GridViewPartial")
        {
            return PartialView(viewName, Mgr.GetItems());
        }

        protected virtual ActionResult AddNew(T item, string viewName = "_GridViewPartial")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mgr.Add(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return PartialView(viewName, Mgr.GetItems());
        }

        protected virtual ActionResult Update(T item, string viewName = "_GridViewPartial")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mgr.Update(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return PartialView(viewName, Mgr.GetItems());
        }

        protected virtual ActionResult Delete(int id, string viewName = "_GridViewPartial")
        {
            if (id >= 0)
            {
                try
                {
                    var item = Mgr.GetItem(id);
                    if (item != null)
                    {
                        Mgr.Delete(id);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView(viewName, Mgr.GetItems());
        }
    }
}