using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egreeting.Web.Controllers.Admin
{
    public class SyncPaymentController : Controller
    {
        // GET: SyncPayment
        [HttpPost]
        public ActionResult Index()
        {
            using (var context = new EgreetingContext())
            {
                var currentDate = DateTime.Now;
                var nextmonthDate = DateTime.Now.AddMonths(1);
                var listUserID = context.Set<Subcriber>().Where(x => x.Draft != true).Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserPaymentThisMonth = context.Set<Payment>()
                                        .Where(x => x.Draft != true 
                                            && x.Month == currentDate.Month
                                            && x.Year == currentDate.Year)
                                        .Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserPaymentNextMonth = context.Set<Payment>()
                                        .Where(x => x.Draft != true
                                            && x.Month == nextmonthDate.Month
                                            && x.Year == nextmonthDate.Year)
                                        .Select(x => x.EgreetingUser.EgreetingUserID).ToList();
                var listUserIDThisMonth = listUserID.Where(x => !listUserPaymentThisMonth.Contains(x)).ToList();
                var listUserIDNextMonth = listUserID.Where(x => !listUserPaymentNextMonth.Contains(x)).ToList();

                var payments = new List<Payment>();
                foreach (var itemID in listUserIDThisMonth)
                {
                    var payment = new Payment
                    {
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        PaymentStatus = false,
                        EgreetingUser = context.Set<EgreetingUser>().Find(itemID),
                        CreatedDate = DateTime.Now,
                    };
                    payments.Add(payment);
                }
                foreach (var itemID in listUserIDNextMonth)
                {
                    var payment = new Payment
                    {
                        Month = nextmonthDate.Month,
                        Year = nextmonthDate.Year,
                        PaymentStatus = false,
                        EgreetingUser = context.Set<EgreetingUser>().Find(itemID),
                        CreatedDate = DateTime.Now,
                    };
                    payments.Add(payment);
                }
                context.Set<Payment>().AddRange(payments);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Payments");
        }
    }
}