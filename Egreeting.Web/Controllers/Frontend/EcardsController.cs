using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class EcardsController : BaseFrontController
    {
        private IEcardBusiness EcardBusiness;
        public EcardsController(IEcardBusiness EcardBusiness)
        {
            this.EcardBusiness = EcardBusiness;
        }

        [Route("Ecards/{slug}")]
        // GET: Ecards/Details/5
        public ActionResult Details(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Ecard ecard = EcardBusiness.All.Where(x => x.EcardSlug.Equals(slug)).FirstOrDefault();
            if (ecard == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.topEcards = EcardBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.Price).Take(12).ToList();
            return View(ViewNamesConstant.FrontendEcardsDetails, ecard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EcardBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
