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
    public class ApiScheduleSendersController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/ScheduleSenders
        public IQueryable<ScheduleSender> GetScheduleSenders()
        {
            return db.ScheduleSenders;
        }

        // GET: api/ScheduleSenders/5
        [ResponseType(typeof(ScheduleSender))]
        public IHttpActionResult GetScheduleSender(int id)
        {
            ScheduleSender ScheduleSender = db.ScheduleSenders.Find(id);
            if (ScheduleSender == null)
            {
                return NotFound();
            }

            return Ok(ScheduleSender);
        }

        // PUT: api/ScheduleSenders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScheduleSender(int id, ScheduleSender ScheduleSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ScheduleSender.ScheduleSenderID)
            {
                return BadRequest();
            }

            db.Entry(ScheduleSender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleSenderExists(id))
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

        // POST: api/ScheduleSenders
        [ResponseType(typeof(ScheduleSender))]
        public IHttpActionResult PostScheduleSender(ScheduleSender ScheduleSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScheduleSenders.Add(ScheduleSender);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ScheduleSender.ScheduleSenderID }, ScheduleSender);
        }

        // DELETE: api/ScheduleSenders/5
        [ResponseType(typeof(ScheduleSender))]
        public IHttpActionResult DeleteScheduleSender(int id)
        {
            ScheduleSender ScheduleSender = db.ScheduleSenders.Find(id);
            if (ScheduleSender == null)
            {
                return NotFound();
            }

            db.ScheduleSenders.Remove(ScheduleSender);
            db.SaveChanges();

            return Ok(ScheduleSender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleSenderExists(int id)
        {
            return db.ScheduleSenders.Count(e => e.ScheduleSenderID == id) > 0;
        }
    }
}