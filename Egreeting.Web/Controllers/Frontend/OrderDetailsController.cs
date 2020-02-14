using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class OrderDetailsController : BaseController
    {
        private IOrderDetailBusiness OrderDetailBusiness;
        public OrderDetailsController(IOrderDetailBusiness OrderDetailBusiness)
        {
            this.OrderDetailBusiness = OrderDetailBusiness;
        }

        // GET: OrderDetails
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendOrderDetailsIndex, OrderDetailBusiness.All.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendOrderDetailsDetails, OrderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendOrderDetailsCreate);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail OrderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetailBusiness.Insert(OrderDetail);
                OrderDetailBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.FrontendOrderDetailsCreate, OrderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendOrderDetailsEdit, OrderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail OrderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetailBusiness.Update(OrderDetail);
                OrderDetailBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.FrontendOrderDetailsEdit, OrderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendOrderDetailsDelete, OrderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            OrderDetailBusiness.Delete(OrderDetail);
            OrderDetailBusiness.Save();
            return RedirectToAction("Index");
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
