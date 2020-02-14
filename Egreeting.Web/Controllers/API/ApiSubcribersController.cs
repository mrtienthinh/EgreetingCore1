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
    public class ApiSubcribersController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/Subcribers
        public IQueryable<Subcriber> GetSubcribers()
        {
            return db.Subcribers;
        }

        // GET: api/Subcribers/5
        [ResponseType(typeof(Subcriber))]
        public IHttpActionResult GetSubcriber(int id)
        {
            Subcriber subcriber = db.Subcribers.Find(id);
            if (subcriber == null)
            {
                return NotFound();
            }

            return Ok(subcriber);
        }

        // PUT: api/Subcribers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubcriber(int id, Subcriber subcriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subcriber.SubcriberID)
            {
                return BadRequest();
            }

            db.Entry(subcriber).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcriberExists(id))
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

        // POST: api/Subcribers
        [ResponseType(typeof(Subcriber))]
        public IHttpActionResult PostSubcriber(Subcriber subcriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subcribers.Add(subcriber);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subcriber.SubcriberID }, subcriber);
        }

        // DELETE: api/Subcribers/5
        [ResponseType(typeof(Subcriber))]
        public IHttpActionResult DeleteSubcriber(int id)
        {
            Subcriber subcriber = db.Subcribers.Find(id);
            if (subcriber == null)
            {
                return NotFound();
            }

            db.Subcribers.Remove(subcriber);
            db.SaveChanges();

            return Ok(subcriber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubcriberExists(int id)
        {
            return db.Subcribers.Count(e => e.SubcriberID == id) > 0;
        }
    }
}