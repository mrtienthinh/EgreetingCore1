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
    public class PaymentsController : BaseAdminController
    {
        private IPaymentBusiness PaymentBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        public PaymentsController(IPaymentBusiness PaymentBusiness, IEgreetingUserBusiness EgreetingUserBusiness)
        {
            this.PaymentBusiness = PaymentBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
        }

        // GET: Payments
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Payment>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = PaymentBusiness.All.Where(x => x.EgreetingUser.Email.Contains(search) && x.Draft != true).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = PaymentBusiness.All.Count(x => x.EgreetingUser.Email.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = PaymentBusiness.All.Count(x => x.Draft != true);
                listModel = PaymentBusiness.All.Where(x => x.Draft != true).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminPaymentsIndex, listModel);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Payment Payment = PaymentBusiness.Find(id);
            if (Payment == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminPaymentsDetails, Payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminPaymentsCreate);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment Payment, int? EgreetingUserID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var egreetingUser = context.Set<EgreetingUser>().Find(EgreetingUserID);
                    if(egreetingUser == null)
                    {
                        ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
                        ModelState.AddModelError(string.Empty, "Need at least one email of user");
                        return View(ViewNamesConstant.AdminPaymentsCreate, Payment);
                    }
                    Payment.EgreetingUser = egreetingUser;
                    Payment.CreatedDate = DateTime.Now;
                    context.Set<Payment>().Add(Payment);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminPaymentsCreate, Payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Payment Payment = PaymentBusiness.Find(id);
            if (Payment == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminPaymentsEdit, Payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment Payment, int? EgreetingUserID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var paymentUpdate = context.Set<Payment>().Find(Payment.PaymentID);
                    if (EgreetingUserID != null && paymentUpdate.EgreetingUser.EgreetingUserID != EgreetingUserID)
                    {
                        var egreetingUser = context.Set<EgreetingUser>().Find(EgreetingUserID);

                        if (egreetingUser != null)
                        {
                            paymentUpdate.EgreetingUser = egreetingUser;
                        }
                    }
                    paymentUpdate.Month = Payment.Month;
                    paymentUpdate.Year = Payment.Year;
                    paymentUpdate.PaymentStatus = Payment.PaymentStatus;
                    paymentUpdate.ModifiedDate = DateTime.Now;
                    context.Set<Payment>().Attach(paymentUpdate);
                    context.Entry(paymentUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.EgreetingUsers = EgreetingUserBusiness.AllNoTracking.Where(x => x.Draft != true).Select(x => new { x.EgreetingUserID, x.Email }).ToDictionary(k => k.EgreetingUserID, v => v.Email);
            return View(ViewNamesConstant.AdminPaymentsEdit, Payment);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Payment Payment = PaymentBusiness.Find(ItemID);
            Payment.Draft = true;
            Payment.ModifiedDate = DateTime.Now;
            PaymentBusiness.Update(Payment);
            PaymentBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PaymentBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
