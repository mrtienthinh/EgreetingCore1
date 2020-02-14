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
    public class ApiEcardsController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/Ecards
        public IQueryable<Ecard> GetEcards()
        {

            return db.Ecards;
        }

        // GET: api/Ecards/5
        [ResponseType(typeof(Ecard))]
        public IHttpActionResult GetEcard(int id)
        {
            Ecard ecard = db.Ecards.Find(id);
            if (ecard == null)
            {
                return NotFound();
            }

            return Ok(ecard);
        }

        // PUT: api/Ecards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEcard(int id, Ecard ecard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ecard.EcardID)
            {
                return BadRequest();
            }

            db.Entry(ecard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EcardExists(id))
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

        // POST: api/Ecards
        [ResponseType(typeof(Ecard))]
        public IHttpActionResult PostEcard(Ecard ecard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ecards.Add(ecard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ecard.EcardID }, ecard);
        }

        // DELETE: api/Ecards/5
        [ResponseType(typeof(Ecard))]
        public IHttpActionResult DeleteEcard(int id)
        {
            Ecard ecard = db.Ecards.Find(id);
            if (ecard == null)
            {
                return NotFound();
            }

            db.Ecards.Remove(ecard);
            db.SaveChanges();

            return Ok(ecard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EcardExists(int id)
        {
            return db.Ecards.Count(e => e.EcardID == id) > 0;
        }
    }
}