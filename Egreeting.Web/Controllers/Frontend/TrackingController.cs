﻿using Egreeting.Business.IBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Egreeting.Web.Controllers.Frontend
{
    public class TrackingController : BaseFrontController
    {
        private IOrderDetailBusiness OrderDetailBusiness;
        public TrackingController(IOrderDetailBusiness OrderDetailBusiness)
        {
            this.OrderDetailBusiness = OrderDetailBusiness;
        }
        // GET: Tracking
        public ActionResult Index()
        {
            return View("~/Views/Frontend/Tracking/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowOrderDetail(int orderID)
        {
            var listOrderDetails = OrderDetailBusiness.All.Include(x => x.Order).Where(x => x.Order.OrderID == orderID).ToList();
            return PartialView("~/Views/Frontend/Tracking/_OrderDetail.cshtml", listOrderDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                OrderDetailBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}