using Microsoft.AspNetCore.Mvc;
using Egreeting.Web.Utils;
using Microsoft.AspNetCore.Identity;

namespace Egreeting.Web.Controllers
{
    public class BaseController : Controller
    {
        public GlobalInfo _globalInfo = null;

        public BaseController()
        {
            _globalInfo = GlobalInfo.getInstance();
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ToString());
            }
        }
    }
}