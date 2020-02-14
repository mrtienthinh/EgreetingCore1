using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using Microsoft.AspNetCore.Mvc;
using Egreeting.Domain;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class OrdersController : BaseController
    {
        private IOrderBusiness OrderBusiness;
        private IEcardBusiness EcardBusiness;
        public OrdersController(IOrderBusiness OrderBusiness, IEcardBusiness EcardBusiness)
        {
            this.OrderBusiness = OrderBusiness;
            this.EcardBusiness = EcardBusiness;
        }

        public ActionResult Index(string ListEcardIDString)
        {
            if (string.IsNullOrEmpty(ListEcardIDString))
            {
                return Redirect("/");
            }
            var listEcardID = ListEcardIDString.Split('-').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToList();
            var listEcard = EcardBusiness.All.Where(x => listEcardID.Contains(x.EcardID)).ToList();
            ViewBag.listEcardIDstring = ListEcardIDString;
            return View(ViewNamesConstant.FrontendOrdersIndex, listEcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order order, string listEcardIDstring)
        {
            if (string.IsNullOrEmpty(order.RecipientEmail)){
                return Json(new { Code = "fail", orderID = "" });
            }
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (var context = new EgreetingContext())
                    {
                        string email = User.Identity.Name;
                        var userSend = context.EgreetingUsers.Where(x => x.Email == email).FirstOrDefault();
                        bool checkPayment = false;
                        if (userSend != null)
                        {
                            checkPayment = userSend.PaymentDueDate > DateTime.Now;
                        }

                        if (checkPayment) {
                            var listEcardID = listEcardIDstring.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                            var listOrderDetails = new List<OrderDetail>();
                            foreach (var ecardID in listEcardID)
                            {
                                var ecard = context.Set<Ecard>().Find(ecardID);
                                var orderDetails = new OrderDetail
                                {
                                    SendStatus = false,
                                    Ecard = ecard,
                                    CreatedDate = DateTime.Now,
                                };
                                listOrderDetails.Add(orderDetails);
                            }

                            order.OrderDetails = listOrderDetails;
                            order.SendStatus = false;
                            order.TotalPrice = listOrderDetails.Select(x => x.Ecard.Price).Sum();
                            order.CreatedDate = DateTime.Now;
                            context.Set<Order>().Add(order);
                            context.SaveChanges();
                            return Json(new { Code = "success", orderID = order.OrderID });
                        }
                        else
                        {
                            return Json(new { Code = "subcriber", orderID = ""});
                        }
                    }
                }
                else
                {
                    return Json(new { Code = "forsending", orderID = "" });
                }
                
            }
            return Json(new { Code = "fail", orderID = "" });
        }

        [HttpPost]
        public ActionResult Create(Order order, string listEcardIDstring)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var listEcardID = listEcardIDstring.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                    var listOrderDetails = new List<OrderDetail>();
                    foreach (var ecardID in listEcardID)
                    {
                        var ecard = context.Set<Ecard>().Find(ecardID);
                        var orderDetails = new OrderDetail
                        {
                            SendStatus = false,
                            Ecard = ecard,
                            CreatedDate = DateTime.Now,
                        };
                        listOrderDetails.Add(orderDetails);
                    }

                    order.OrderDetails = listOrderDetails;
                    order.SendStatus = false;
                    order.TotalPrice = listOrderDetails.Select(x => x.Ecard.Price).Sum();
                    order.CreatedDate = DateTime.Now;
                    context.Set<Order>().Add(order);
                    context.SaveChanges();
                    return Json( new { Code = "success", orderID = order.OrderID});

                }
            }
            return Json("fail");
        }

        public ActionResult RemoveCart()
        {
            return View(ViewNamesConstant.FrontendOrdersRemoveCart);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                OrderBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
