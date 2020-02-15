using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Admin
{
    [Route("admin/[controller]/[action]")]
    public class HomeController : BaseAdminController
    {
        public ActionResult Index()
        {
            return Redirect("/categories/");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}