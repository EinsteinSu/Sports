using System.Web.Mvc;
using Sports_Website.Models;

namespace Sports.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            
            return View();
        }
        
        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        }
    
    }
}