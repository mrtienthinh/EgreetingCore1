using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class SubcribersController : BaseFrontController
    {
        private ISubcriberBusiness SubcriberBusiness;
        public SubcribersController(ISubcriberBusiness SubcriberBusiness)
        {
            this.SubcriberBusiness = SubcriberBusiness;
        }

        // GET: Subcribers
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(ViewNamesConstant.FrontendSubcribersIndex);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SubcriberBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
