using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class FeedbacksController : BaseAdminController
    {
        private IFeedbackBusiness FeedbackBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEcardBusiness EcardBusiness;

        public FeedbacksController(IFeedbackBusiness FeedbackBusiness, IEgreetingUserBusiness EgreetingUserBusiness, IEcardBusiness EcardBusiness)
        {
            this.FeedbackBusiness = FeedbackBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            this.EcardBusiness = EcardBusiness;
        }

        // GET: Feedbacks
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Feedback>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = FeedbackBusiness.All.Where(x => x.Subject.Contains(search) && x.Draft != true).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => x.Subject.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => x.Draft != true);
                listModel = FeedbackBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminFeedbacksIndex, listModel);
        }

        // GET: Feedbacks/Details/5
        [Route("{id:int:min(1)}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Feedback feedback = FeedbackBusiness.Find(id);
            if (feedback == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.AdminFeedbacksDetails,feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.Ecards = EcardBusiness.All.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            return View(ViewNamesConstant.AdminFeedbacksCreate);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback, int? EcardID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    var user = new EgreetingUser();
                    if (HttpContext.User != null)
                    {
                        string email = HttpContext.User.Identity.Name;
                        user = context.Set<EgreetingUser>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                    }
                    if (user != null)
                        feedback.EgreetingUser = user;

                    var ecard = context.Set<Ecard>().Find(EcardID);
                    if (ecard != null)
                        feedback.Ecard = ecard;

                    feedback.CreatedDate = DateTime.Now;
                    context.Set<Feedback>().Add(feedback);
                    context.SaveChanges();
                }
                    
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminFeedbacksCreate,feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Feedback feedback = FeedbackBusiness.Find(ItemID);
            feedback.Draft = true;
            FeedbackBusiness.Update(feedback);
            FeedbackBusiness.Save();
            return RedirectToAction("Index");
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
