using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class FeedbacksController : BaseController
    {
        private IFeedbackBusiness FeedbackBusiness;
        public FeedbacksController(IFeedbackBusiness FeedbackBusiness)
        {
            this.FeedbackBusiness = FeedbackBusiness;
        }

        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendFeedbacksIndex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback, int? EcardID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = new EgreetingUser();
                        if (HttpContext.User != null)
                        {
                            string email = User.Identity.Name;
                            user = context.Set<EgreetingUser>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                            if (user != null)
                                feedback.EgreetingUser = user;
                        }
                    }

                    var ecard = context.Set<Ecard>().Find(EcardID);
                    if (ecard != null)
                        feedback.Ecard = ecard;

                    feedback.CreatedDate = DateTime.Now;
                    context.Set<Feedback>().Add(feedback);
                    context.SaveChanges();
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FeedbackBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
