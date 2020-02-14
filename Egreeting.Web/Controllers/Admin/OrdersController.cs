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
    public class OrdersController : BaseAdminController
    {
        private IOrderBusiness OrderBusiness;
        private IEcardBusiness EcardBusiness;

        public OrdersController(IOrderBusiness OrderBusiness, IEcardBusiness EcardBusiness)
        {
            this.OrderBusiness = OrderBusiness;
            this.EcardBusiness = EcardBusiness;
        }

        // GET: Orders
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Order>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = OrderBusiness.All.Where(x => x.SendSubject.Contains(search) && x.Draft != true).OrderByDescending(x => x.OrderID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = OrderBusiness.All.Count(x => x.SendSubject.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = OrderBusiness.All.Count(x => x.Draft != true);
                listModel = OrderBusiness.All.Where(x => x.Draft != true).OrderByDescending(x => x.OrderID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminOrdersIndex, listModel);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Order Order = OrderBusiness.Find(id);
            if (Order == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminOrdersDetails, Order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminOrdersCreate);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order Order, string ListEcardString)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var listEcardID = ListEcardString.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
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

                    Order.OrderDetails = listOrderDetails;
                    Order.SendStatus = false;
                    Order.TotalPrice = listOrderDetails.Select(x => x.Ecard.Price).Sum();
                    Order.CreatedDate = DateTime.Now;
                    context.Set<Order>().Add(Order);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminOrdersCreate, Order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Order Order = OrderBusiness.Find(id);
            if (Order == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminOrdersEdit, Order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order Order, string ListEcardString)
        {
            if(string.IsNullOrEmpty(Order.RecipientEmail)){
                ModelState.AddModelError("RecipientEmail", "Recipient's email can't not be empty!");
            }
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var orderUpdate = context.Set<Order>().Find(Order.OrderID);

                    
                    if(orderUpdate == null)
                    {
                        ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
                        ModelState.AddModelError(string.Empty, "Order not found!");
                        return View(ViewNamesConstant.AdminOrdersEdit, Order);
                    }

                    // thinh: check sending status
                    if ( orderUpdate.SendStatus == true || orderUpdate.OrderDetails.Any(x => x.Draft != true && x.SendStatus))
                    {
                        ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
                        ModelState.AddModelError(string.Empty, "Can't edit because ecard had been sended!");
                        return View(ViewNamesConstant.AdminOrdersEdit, Order);
                    }

                    // thinh: remove order detail
                    foreach (var orderDetailRemove in orderUpdate.OrderDetails)
                    {
                        orderDetailRemove.Draft = true;
                    }

                    // thinh: update order detail
                    var listEcardID = ListEcardString.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                    var listOrderDetails = new List<OrderDetail>();
                    foreach (var ecardID in listEcardID)
                    {
                        var ecard = context.Set<Ecard>().Find(ecardID);
                        var orderDetails = new OrderDetail
                        {
                            SendStatus = Order.SendStatus,
                            Ecard = ecard,
                            CreatedDate = DateTime.Now,
                        };
                        listOrderDetails.Add(orderDetails);
                    }
                    orderUpdate.OrderDetails = listOrderDetails;

                    // thinh: update order
                    orderUpdate.SendSubject = Order.SendSubject;
                    orderUpdate.SendStatus = Order.SendStatus;
                    orderUpdate.SenderName = Order.SenderName;
                    orderUpdate.RecipientEmail = Order.RecipientEmail;
                    orderUpdate.SendMessage = Order.SendMessage;
                    orderUpdate.ScheduleTime = Order.ScheduleTime;
                    orderUpdate.ModifiedDate = DateTime.Now;

                    context.Set<Order>().Attach(orderUpdate);
                    context.Entry(orderUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ecards = EcardBusiness.AllNoTracking.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminOrdersEdit, Order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Order Order = OrderBusiness.Find(ItemID);
            Order.Draft = true;
            Order.ModifiedDate = DateTime.Now;

            foreach (var orderDetail in Order.OrderDetails)
            {
                orderDetail.Draft = true;
                orderDetail.ModifiedDate = DateTime.Now;
            }
            OrderBusiness.Update(Order);
            OrderBusiness.Save();
            return RedirectToAction("Index");
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
