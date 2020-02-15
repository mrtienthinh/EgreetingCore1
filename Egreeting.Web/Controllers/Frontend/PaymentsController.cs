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
    public class PaymentsController : BaseFrontController
    {
        private IPaymentBusiness PaymentBusiness;
        public PaymentsController(IPaymentBusiness PaymentBusiness)
        {
            this.PaymentBusiness = PaymentBusiness;
        }

        // GET: Payments
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendPaymentsIndex);
        }

        public ActionResult Subcriber()
        {
            return View(ViewNamesConstant.FrontendPaymentsSubcriber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subcriber(string cardNumber, string email,string expityMonth, string expityYear, string CV)
        {
            int n = 0;
            long m = 0;
            if (cardNumber.Length != 12 || !long.TryParse(cardNumber, out m))
            {
                ModelState.AddModelError(string.Empty, "Card's not valid!");
            }
            if (expityMonth.Length > 2 || !int.TryParse(expityMonth, out n) || n > 12 || n < DateTime.Now.Month)
            {
                ModelState.AddModelError(string.Empty, "Month not valid!");
            }
            if (expityYear.Length != 4|| !int.TryParse(expityYear, out n) || n < DateTime.Now.Year)
            {
                ModelState.AddModelError(string.Empty, "Year not valid!");
            }
            if (CV.Length != 3 || !int.TryParse(CV, out n))
            {
                ModelState.AddModelError(string.Empty, "CVG not valid!");
            }
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                if(!context.Set<Subcriber>().Any(x => x.Email.Equals(email)))
                {
                    ModelState.AddModelError(string.Empty, "Subcriber not found!");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(ViewNamesConstant.FrontendPaymentsSubcriber);
            }
            var listPayment = new List<int>();
            DateTime maxMonth = DateTime.Now;
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                listPayment = context.Set<Payment>().Where(x => x.EgreetingUser.Email.Equals(email)
                    && x.Draft != true
                    && x.PaymentStatus == false
                    && x.Month >= DateTime.Now.Month
                    && x.Year == DateTime.Now.Year
                    ).OrderBy(x => x.Year).ThenBy(x => x.Month).Select(x => x.PaymentID).ToList();
                if (listPayment.Count > 0)
                {
                    var paymentMonthMax = context.Set<Payment>().Where(x => x.EgreetingUser.Email.Equals(email)
                        && x.Draft != true
                        && x.PaymentStatus == false
                        && x.Month >= DateTime.Now.Month
                        && x.Year == DateTime.Now.Year
                        ).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).Take(1).FirstOrDefault();
                    maxMonth = new DateTime(paymentMonthMax.Year, paymentMonthMax.Month, 1);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Subcriber has paid for this month!");
                    return View(ViewNamesConstant.FrontendPaymentsSubcriber);
                }
            }
            foreach (var id in listPayment)
            {
                var item = PaymentBusiness.Find(id);
                item.PaymentStatus = true;
                item.ModifiedDate = DateTime.Now;
                item.EgreetingUser.PaymentDueDate = maxMonth.AddMonths(1);
                PaymentBusiness.Update(item);
                PaymentBusiness.Save();
            }
            return Redirect("/");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForSending(string cardNumber, string expityMonth, string expityYear, string CV)
        {
            int n = 0;
            long m = 0;
            if (cardNumber.Length != 12 || !long.TryParse(cardNumber, out m))
            {
                return Json("fail");
            }
            if (expityMonth.Length > 2 || !int.TryParse(expityMonth, out n) || n > 12 || n < DateTime.Now.Month)
            {
                return Json("fail");
            }
            if (expityYear.Length != 4 || !int.TryParse(expityYear, out n) || n < DateTime.Now.Year)
            {
                return Json("fail");
            }
            if (CV.Length != 3 || !int.TryParse(CV, out n))
            {
                return Json("fail");
            }
            return Json("success");
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
