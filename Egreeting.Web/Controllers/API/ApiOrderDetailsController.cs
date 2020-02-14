using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;

namespace Egreeting.Web.Controllers.API
{
    public class ApiOrderDetailsController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/OrderDetails
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult GetOrderDetail(int id)
        {
            OrderDetail OrderDetail = db.OrderDetails.Find(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Ok(OrderDetail);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetail(int id, OrderDetail OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != OrderDetail.OrderDetailID)
            {
                return BadRequest();
            }

            db.Entry(OrderDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult PostOrderDetail(OrderDetail OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(OrderDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = OrderDetail.OrderDetailID }, OrderDetail);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public IHttpActionResult DeleteOrderDetail(int id)
        {
            OrderDetail OrderDetail = db.OrderDetails.Find(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(OrderDetail);
            db.SaveChanges();

            return Ok(OrderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderDetailID == id) > 0;
        }
    }
}