using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HttpError404()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult HttpError403()
        {
            return View();
        }

        public ActionResult General()
        {
            return View();
        }
    }
}