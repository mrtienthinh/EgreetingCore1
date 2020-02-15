using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class ScheduleSendersController : BaseAdminController
    {
        private IScheduleSenderBusiness ScheduleSenderBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEcardBusiness EcardBusiness;
        public ScheduleSendersController(IScheduleSenderBusiness ScheduleSenderBusiness, IEgreetingUserBusiness EgreetingUserBusiness, IEcardBusiness EcardBusiness)
        {
            this.ScheduleSenderBusiness = ScheduleSenderBusiness;
            this.EcardBusiness = EcardBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;

        }

        // GET: ScheduleSenders
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<ScheduleSender>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = ScheduleSenderBusiness.All.Where(x => x.EgreetingUser.Email.Contains(search) && x.Draft != true).OrderBy(x => x.ScheduleSenderID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = ScheduleSenderBusiness.All.Count(x => x.EgreetingUser.Email.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = ScheduleSenderBusiness.All.Count(x => x.Draft != true);
                listModel = ScheduleSenderBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.ScheduleSenderID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminScheduleSendersIndex, listModel);
        }

        // GET: ScheduleSenders/Details/5
        [Route("{id:int:min(1)}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            if (ScheduleSender == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminScheduleSendersDetails, ScheduleSender);
        }

        // GET: ScheduleSenders/Create
        public ActionResult Create()
        {
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminScheduleSendersCreate);
        }

        // POST: ScheduleSenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScheduleSender ScheduleSender, int? EcardID, int? EgreetingUserID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    // thinh: check user exist
                    var egreetingUser = new EgreetingUser();
                    if(EcardID != null)
                    {
                        egreetingUser = context.Set<EgreetingUser>().Find(EgreetingUserID);
                        if(egreetingUser == null)
                        {
                            ModelState.AddModelError(string.Empty, "User not found!");
                            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                            return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Need at least one user!");
                        ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                        ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                        return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                    }

                    // thinh: check ecard exist
                    var ecard = new Ecard();
                    if (EcardID != null)
                    {
                        ecard = context.Set<Ecard>().Find(EcardID);
                        if (ecard == null)
                        {
                            ModelState.AddModelError(string.Empty, "Ecard not found!");
                            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                            return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Need at least one ecard!");
                        ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                        ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                        return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                    }

                    // thinh: create schedule sender
                    ScheduleSender.Ecard = ecard;
                    ScheduleSender.EgreetingUser = egreetingUser;
                    ScheduleSender.CreatedDate = DateTime.Now;

                    context.Set<ScheduleSender>().Add(ScheduleSender);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminScheduleSendersCreate,ScheduleSender);
        }

        // GET: ScheduleSenders/Edit/5
        [Route("{id:int:min(1)}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            if (ScheduleSender == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminScheduleSendersEdit,ScheduleSender);
        }

        // POST: ScheduleSenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScheduleSender ScheduleSender, int? EcardID, int? EgreetingUserID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    var scheduleSenderUpdate = context.Set<ScheduleSender>().Find(ScheduleSender.ScheduleSenderID);
                    // thinh: check user exist
                    if (EcardID != null)
                    {
                        var egreetingUser = context.Set<EgreetingUser>().Find(EgreetingUserID);
                        if (egreetingUser == null)
                        {
                            ModelState.AddModelError(string.Empty, "User not found!");
                            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                            return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                        }
                        scheduleSenderUpdate.EgreetingUser = egreetingUser;
                    }

                    // thinh: check ecard exist
                    if (EcardID != null)
                    {
                        var ecard = context.Set<Ecard>().Find(EcardID);
                        if (ecard == null)
                        {
                            ModelState.AddModelError(string.Empty, "Ecard not found!");
                            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
                            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                            return View(ViewNamesConstant.AdminScheduleSendersCreate, ScheduleSender);
                        }
                        scheduleSenderUpdate.Ecard = ecard;
                    }

                    // thinh: create schedule sender
                    scheduleSenderUpdate.ScheduleTime = ScheduleSender.ScheduleTime;
                    scheduleSenderUpdate.ScheduleType = ScheduleSender.ScheduleType;
                    scheduleSenderUpdate.SenderName = ScheduleSender.SenderName;
                    scheduleSenderUpdate.RecipientEmail = ScheduleSender.RecipientEmail;
                    scheduleSenderUpdate.SendSubject = ScheduleSender.SendSubject;
                    scheduleSenderUpdate.SendMessage = ScheduleSender.SendMessage;
                    scheduleSenderUpdate.ModifiedDate = DateTime.Now;

                    context.Set<ScheduleSender>().Attach(scheduleSenderUpdate);
                    context.Entry(scheduleSenderUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminScheduleSendersEdit,ScheduleSender);
        }

        // POST: ScheduleSenders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(ItemID);
            ScheduleSender.ModifiedDate = DateTime.Now;
            ScheduleSender.Draft = true;
            ScheduleSenderBusiness.Update(ScheduleSender);
            ScheduleSenderBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ScheduleSenderBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
